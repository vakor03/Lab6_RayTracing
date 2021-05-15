using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace RayProcessor.Lib
{
    public class FileManager
    {
        private string _path;
        private Screen _screen;

        public FileManager(string path, Screen screen)
        {
            _path = path;
            _screen = screen;
        }

        public List<Face> ReadObj()
        {
            List<Point> verts = new List<Point>();
            List<Face> faces = new List<Face>();
            using StreamReader streamReader = new StreamReader(_path);
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
                        idArray[i - 1] = id-1;
                    }

                    faces.Add(new Face(new[] {verts[idArray[0]], verts[idArray[1]], verts[idArray[2]]}));
                }
            }

            return faces;
        }

        public void WriteBMP(string path)
        {
            int countOfZeroBits = 3-(Convert.ToInt32(_screen.screenPixelSize.width)*3-1)%4;
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                writer.Write('B');
                writer.Write('M');
                writer.Write(Convert.ToUInt32(_screen.screenPixelSize.width*_screen.screenPixelSize.height+54));
                writer.Write((UInt16)0);
                writer.Write((UInt16)0);
                writer.Write((UInt32)54);
                writer.Write((UInt32)40);
                writer.Write((UInt32)_screen.screenPixelSize.width);
                writer.Write((UInt32)_screen.screenPixelSize.height);
                writer.Write((UInt16)1);
                writer.Write((UInt16)24);
                for (int i = 0; i < 6; i++)
                {
                    writer.Write((UInt32)0);
                }
                for (int i = 0; i < _screen.screenPixelSize.height; i++)
                {
                    for (int j = 0; j < _screen.screenPixelSize.width; j++)
                    {
                        writer.Write((byte)_screen.pixels[i,j]*255);
                        writer.Write((byte)_screen.pixels[i,j]*255);
                        writer.Write((byte)_screen.pixels[i,j]*255);
                    }
                    for (int j = 0; j < countOfZeroBits; j++)
                    {
                        writer.Write((byte)0);
                    }
                }
            }
        }
    }
}