using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace RayProcessor.Lib
{
    public class FileManager
    {
        public FileManager()
        {
        }

        public void ReadObj(string path, Tree tree)
        {
            List<Point> verts = new List<Point>();
            List<Triangle> faces = new List<Triangle>();
            using StreamReader streamReader = new StreamReader(path);
            string inputStr;
            while ((inputStr = streamReader.ReadLine()) != null)
            {
                if (inputStr.Length > 0 && inputStr[0] == 'v' && inputStr[1] != 't')
                {
                    string[] strings = inputStr.Split(' ');
                    double[] coords = new double[3];
                    for (int i = 1; i < 4; i++)
                    {
                        IFormatProvider formatter = new NumberFormatInfo {NumberDecimalSeparator = "."};
                        coords[i - 1] = double.Parse(strings[i], formatter);
                    }

                    verts.Add(new Point(coords[0], coords[1], coords[2]));
                }
                else if (inputStr.Length > 0 && inputStr[0] == 'f')
                {
                    string[] strings = inputStr.Split(' ');
                    int[] idArray = new int[3];
                    for (int i = 1; i < 4; i++)
                    {
                        int id = int.Parse(strings[i].Split('/')[0]);
                        idArray[i - 1] = id - 1;
                    }
                    tree.AddTriangle(new Triangle(verts[idArray[0]], verts[idArray[1]], verts[idArray[2]]));
                    //faces.Add(new Triangle(verts[idArray[0]], verts[idArray[1]], verts[idArray[2]]));
                }
            }
            tree.AddTriangle(new Triangle(new Point(10, 10, -0.5), new Point(-10, 0, -0.5), new Point(0, -10, -0.5)));


           // return faces;
        }

        public void WriteBMP(string path, Camera camera)
        {
            int countOfZeroBits = 3 - (Convert.ToInt32(camera.screenPixelSize.width) * 3 - 1) % 4;
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                writer.Write('B');
                writer.Write('M');
                writer.Write(Convert.ToUInt32(camera.screenPixelSize.width * camera.screenPixelSize.height + 54));
                writer.Write((UInt16) 0);
                writer.Write((UInt16) 0);
                writer.Write((UInt32) 54);
                writer.Write((UInt32) 40);
                writer.Write((UInt32) camera.screenPixelSize.width);
                writer.Write((UInt32) camera.screenPixelSize.height);
                writer.Write((UInt16) 1);
                writer.Write((UInt16) 24);
                for (int i = 0; i < 6; i++)
                {
                    writer.Write((UInt32) 0);
                }

                for (int i = 0; i < camera.screenPixelSize.height; i++)
                {
                    for (int j = 0; j < camera.screenPixelSize.width; j++)
                    {
                        writer.Write((byte) (camera.pixels[i, j] * 255));
                        writer.Write((byte) (camera.pixels[i, j] * 255));
                        writer.Write((byte) (camera.pixels[i, j] * 255));
                    }

                    for (int j = 0; j < countOfZeroBits; j++)
                    {
                        writer.Write((byte) 0);
                    }
                }
            }
        }
    }
}