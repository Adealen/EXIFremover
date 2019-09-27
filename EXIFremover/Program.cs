using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace ExifRemover
{
    class Program
    {
        static void Main(string[] args)

        {
            string picExtension = "jpg";
            string inFolder = "In";
            string outFolder = "Out";
            if (args.Length == 1)
            {
                picExtension = args[1];
            }
            if (args.Length == 3)
            {
                picExtension = args[1];
                inFolder = args[2];
                outFolder = args[3];
            }
            int count = 0;
            try
            {
                count = Directory.GetFiles(inFolder, "*." + picExtension).Length;
            }
            catch (System.Exception)
            {
                System.Console.Error.WriteLine("Unable to find input folder, exiting ...");
                System.Environment.Exit(1);
            }
            int actual = 0;
            foreach (string img in Directory.GetFiles(inFolder, "*." + picExtension))
            {
                actual++;
                Image photo = Image.FromFile(img);
                foreach (int ID in photo.PropertyIdList)
                {
                    PropertyItem item = photo.GetPropertyItem(ID);
                    item.Value = System.Text.ASCIIEncoding.ASCII.GetBytes("");
                    photo.SetPropertyItem(item);
                    System.Console.WriteLine("Removed property " + item.Id + " from file " + Path.GetFileName(img));
                }
                System.Console.WriteLine(img + " Done" + "(" + actual + " of " + count +")");
                Directory.CreateDirectory(outFolder);
                photo.Save(outFolder + "//" + Path.GetFileName(img), photo.RawFormat);
                photo.Dispose();
            }
            string[] ing = Directory.GetFiles("In", "*.jpg");
            System.Console.WriteLine("End...");
        }
    }
}
