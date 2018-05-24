var ParkingLocalizer = function (lat_depart, long_depart, lat_arrive, long_arrive, url) {
    this._latDepart = lat_depart;
    this._longDepart = long_depart;
    this._latArrive = lat_arrive;
    this._longArrive = long_arrive;
    this._apiUrl = url;
    this._parkingList = null;
    this._parkingList3 = null;

    this._oDataApi = {};

    var _self = this;

    /**
    * Met à jour la latitude et la longitude du point de départ
    */
    this.updateCoordDepart = function (lat_depart, long_depart) {
        _self._latDepart = lat_depart;
        _self._longDepart = long_depart;
    };

    this.get3ParkingsLoaded = function () {
        return _self._parkingList3;
    };

    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    //:::                                                                         :::
    //:::  This routine calculates the distance between two points (given the     :::
    //:::  latitude/longitude of those points). It is being used to calculate     :::
    //:::  the distance between two locations using GeoDataSource (TM) prodducts  :::
    //:::                                                                         :::
    //:::  Definitions:                                                           :::
    //:::    South latitudes are negative, east longitudes are positive           :::
    //:::                                                                         :::
    //:::  Passed to function:                                                    :::
    //:::    lat1, lon1 = Latitude and Longitude of point 1 (in decimal degrees)  :::
    //:::    lat2, lon2 = Latitude and Longitude of point 2 (in decimal degrees)  :::
    //:::    unit = the unit you desire for results                               :::
    //:::           where: 'M' is statute miles (default)                         :::
    //:::                  'K' is kilometers                                      :::
    //:::                  'N' is nautical miles                                  :::
    //:::                                                                         :::
    //:::  Worldwide cities and other features databases with latitude longitude  :::
    //:::  are available at https://www.geodatasource.com                          :::
    //:::                                                                         :::
    //:::  For enquiries, please contact sales@geodatasource.com                  :::
    //:::                                                                         :::
    //:::  Official Web site: https://www.geodatasource.com                        :::
    //:::                                                                         :::
    //:::               GeoDataSource.com (C) All Rights Reserved 2017            :::
    //:::                                                                         :::
    this.distance = function (lat1, lon1, lat2, lon2, unit) {
        var radlat1 = Math.PI * lat1 / 180
        var radlat2 = Math.PI * lat2 / 180
        var theta = lon1 - lon2
        var radtheta = Math.PI * theta / 180
        var dist = Math.sin(radlat1) * Math.sin(radlat2) + Math.cos(radlat1) * Math.cos(radlat2) * Math.cos(radtheta);
        dist = Math.acos(dist)
        dist = dist * 180 / Math.PI
        dist = dist * 60 * 1.1515
        if (unit == "K") { dist = dist * 1.609344 }
        if (unit == "N") { dist = dist * 0.8684 }
        return dist
    };

    /**
    * Parse le CSV vers un tableau d'objets
    */
    this.CSVtoArray = function (text) {
        var lines = text.split('\r\n');
        var ouput = [];
        var keys = {};
        var reg = /(\".*;.*\")/;

        $.each(lines, function (index, banana) {
            // Fix le format de ces branques qui fournis le csv
            var merde = banana.match(reg);
            if (merde !== null) {
                $.each(merde, function (i, d) {
                    var r = d.replace(/;/, '|');
                    r = r.replace(/\"/g, '');
                    
                    banana = banana.replace(d, r);
                });
            }

            var row = banana.split(';');
            if (row.length > 1) {
                var o = {};
                $.each(row, function(idx, data){
                    if (index == 0) {
                        keys[idx] = data;
                    } else {
                        var lbl = keys[idx];
                        o[lbl] = data;
                    }
                });
                if (index > 0) {
                    ouput.push(o);
                }
            }
        });
        return ouput;
    };

    /**
    * Charge les données de l'api dans l'instance de l'objet
    */
    this.loadApi = function (callback) {
        return new Promise(function () {
            var self = this;
            $.ajax({
                url: _self._apiUrl + '?crs=EPSG:4326',
                method: 'get'
            }).done(function (response) {
                // Regroupe les résultas
                if (response !== null) {
                    $.each(response.parks, function (index, data) {
                        _self._oDataApi[data.id] = data;
                    });
                    $.each(response.features.features, function (index, data) {
                        _self._oDataApi[data.id].features = data;
                    });
                    callback();
                } else {
                    throw "Erreur : aucune réponse de l'api";
                }
            }).fail(function (response) {
                callback(false, response);
            });
        });
    };

    /**
    * Retourne la liste des 3 premiers parkings éligible
    * TODO : réparer l'algo à la fin, ça me donne le tournis.
    */
    this.getParkingList = function () {
        var parkEvent = {};
        var depEvent = {};
        $.each(_self._oDataApi, function (index, data) {
            // Parking les plus proche de l'événement
            var distParkArrive = _self.distance(data.features.geometry.coordinates[1], data.features.geometry.coordinates[0], _self._latArrive, _self._longArrive, 'K');
            var distParkDepart = _self.distance(data.features.geometry.coordinates[1], data.features.geometry.coordinates[0], _self._latDepart, _self._longDepart, 'K');
            parkEvent[distParkArrive] = data;
            depEvent[distParkDepart] = data;
        });
        
        var k = Object.keys(parkEvent);
        var sortedk = k.sort();
        var sortedPark = {};
        $.each(sortedk, function (index, data) {
            var o = parkEvent[data];
            if (o.parkInformation.free > 10) {
                sortedPark[data] = parkEvent[data];
            }
        });

        k = Object.keys(depEvent);
        sortedk = k.sort();
        var sortedDep = {};
        $.each(sortedk, function (index, data) {
            var o = depEvent[data];
            if (o.parkInformation.free > 10) {
                sortedDep[data] = depEvent[data];
            }
        });

        var result = [];
        // Récupère les 3 plus proche de l'événement
        $.each(sortedPark, function (dist1, data) {
            if (result.length < 3) {
                result.push(data);
            }
        });
        _self._parkingList = result;
        return result;
    };

    /**
    * Injecte les tarifs horaires dans les parkings
    * Appel le callback quand fini
    */
    this.getParkingPrice = function (parkings, heureOuvert, heureFerme, callback) {
        $.ajax({
            url: _self._apiUrl + "/timetable-and-prices",
            method: 'get'
        }).done(function (response) {
            var cvsdata = _self.CSVtoArray(response);
            // Récupère les horaires de chacun des parkings données
            $.each(parkings, function (index, data) {
                var tarifIdx = cvsdata.map(function (a) { return a.Parking; }).indexOf(data.id);
                if (tarifIdx > -1) {
                    var tarifs = cvsdata[tarifIdx];
                    data.tarifsBrut = tarifs;
                    var tp24h = tarifs.Tarifs.match(/LES PREMIERES 24h[ ]?,(.*)AU-DELA/);
                    var tad = tarifs.Tarifs.match(/AU-DELA de 24h[ ]?:[ ]?(.*)/);
                    var horaires = {};
                    horaires.m24h = [];
                    horaires.p24h = [];

                    if (tp24h !== null && tad !== null && tp24h.length > 0 && tad.length > 0) {
                        tp24h = tp24h[1];
                        tad = tad[1];
                        // Extraction des horaires tp24h
                        var tranches = tp24h.split('//');
                        $.each(tranches, function (i, d) {
                            var o = {};
                            var hor = d.match(/(\d+)h[ ]?à[ ]?(\d+)h/);
                            var prix = d.match(/(\d+[,]?\d+)€/);
                            var multipl = d.match(/(\d+[\/]?\d+) h/);
                            if (multipl === null) {
                                multipl = d.match(/[ ]?\/[ ]?(\d+)h/);
                            }

                            if (hor !== null && prix !== null) {
                                o.start = parseInt(hor[1]);
                                o.end = parseInt(hor[2]);
                                o.price = parseFloat(prix[1].replace(',', '.'));
                                if (multipl !== null) {
                                    o.multiplier = eval(multipl[1]);
                                }
                                horaires.m24h.push(o);

                            }
                        });
                        

                        var prix = tad.match(/(\d+[,]?\d+)€/);
                        var multiple = tad.match(/(\d+[\/]?\d+) h/);
                        if (multiple === null) {
                            multiple = tad.match(/[ ]?\/[ ]?(\d+)h/);
                        }
                        if (prix !== null) {
                            var o = {
                                price: parseFloat(prix[1].replace(',', '.')),
                            };
                            
                            if (multiple !== null) {
                                o.multiplier = eval(multiple[1]);
                            }
                            horaires.p24h = o;
                        }
                    }
                    data.horaires = horaires;
                }
            });
            callback(parkings);
        }).fail(function (response) {
            console.log(response);
        });
    };

    /**
    * Calcul les prix selon les parkings et horaires fournis
    */
    this.calculatePrice = function (parkings, heureOuverture, heureFermeture, nbJour) {
        heureOuverture = parseInt(heureOuverture.replace(':', '.'));
        heureFermeture = parseInt(heureFermeture.replace(':', '.'));
        $.each(parkings, function (idex, data) {
            if (heureOuverture &&
                heureFermeture &&
                heureOuverture != heureFermeture &&
                typeof data.horaires != 'undefined' &&
                data.horaires.m24h !== null &&
                data.horaires.m24h.length > 0)
            {
                // Moins de 24h
                var firstH = data.horaires.m24h[0];
                var lastH = data.horaires.m24h[1];
                if (heureOuverture >= firstH.start && heureOuverture < firstH.end) {
                    // Premier horaire
                    var duree = heureFermeture - heureOuverture;
                    if (firstH.multiplier) {
                        var factor = duree / firstH.multiplier;
                    } else factor = duree;

                    var prix = firstH.price * factor;
                    data.coutEstime = Math.round(prix, 2);
                } else {
                    // Dernier horaire
                    var duree = heureOuverture - heureFermeture;
                    if (lastH.multiplier) {
                        var factor = duree / lastH.multiplier;
                    } else factor = duree;

                    var prix = lastH.price * factor;
                    data.coutEstime = Math.round(prix, 2);
                }
            } else {
                // Plus de 24h
                var duree = nbJour * 24;
                if (nbJour && typeof data.horaires != 'undefined' && data.horaires.p24h !== null) {
                    if (data.horaires.p24h.multiplier) {
                        var factor = duree / data.horaires.p24h.multiplier;
                    } else factor = duree;
                    var prix = data.horaires.p24h.price * factor;
                    data.coutEstime = Math.round(prix, 2);
                }
            }
        });

        _self._parkingList3 = parkings;
        return parkings;
    };

};

/**
* Gère le chaînage des appels des méthodes de l'objets.
*/
ParkingLocalizer.prototype.init = function (heureOuverture, heureFermeture, duree, endCallBack) {
    var self = this;
    this.loadApi(function (response) {
        var liste = self.getParkingList();
        self.getParkingPrice(liste, heureOuverture, heureFermeture, function(result){
            var res = self.calculatePrice(result, heureOuverture, heureFermeture, duree);
            endCallBack(res);
        });
    });
};
