using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ImageDAO : AbsDAO<Image>
    {
        private static readonly ImageDAO instance = new ImageDAO();

        static ImageDAO()
        {
        }

        private ImageDAO()
        {
        }

        public static ImageDAO Instance
        {
            get
            {
                return instance;
            }
        }

        public override bool delete(Image obj)
        {
            var isOk = true;
            try
            {
                var dbO = dbContext.Images.Find(obj.Id);
                dbContext.Images.Remove(dbO);
                dbContext.SaveChanges();

            }
            catch (Exception e)
            {
                isOk = false;
            }
            return isOk;
        }

        public override Image getById(int id)
        {
            return dbContext.Images.Find(id);
        }

        public override Image insert(Image obj)
        {
            dbContext.Images.Add(obj);
            dbContext.SaveChanges();
            return obj;
        }

        public override List<Image> listAll()
        {
            return dbContext.Images.ToList();
        }

        public override bool update(Image obj)
        {
            var isOk = true;
            try
            {
                var old = dbContext.Images.Find(obj.Id);
                old.duplicate(obj);
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                isOk = false;
            }
            return isOk;
        }

        public Image insertFromUrl(string url)
        {
            var img = new Image { Url = url };
            img = this.insert(img);
            return img;
        }
    }
}
