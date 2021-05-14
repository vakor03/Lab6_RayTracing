﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using RayProcesssor.Lib;

namespace RayProcessor.Lib
{
    public class FileManager
    {
        private string _path;

        public FileManager(string path)
        {
            _path = path;
        }

        public List<Face> ReadObj()
        {
            List<Point> verts = new List<Point>();
            List<Face> faces = new List<Face>();
            using StreamReader streamReader = new StreamReader(_path);
            string inputStr;
            while ((inputStr = streamReader.ReadLine())!=null)
            {
                if (inputStr[0]=='v'&& inputStr[1]!='t')
                {
                    string[] strings = inputStr.Split(' ');
                    double[] coords = new double[3];
                    for (int i = 1; i < 4; i++)
                    {
                        IFormatProvider formatter = new NumberFormatInfo {NumberDecimalSeparator = "."};
                        coords[i - 1] = double.Parse(strings[i], formatter);
                    }
                    verts.Add(new Point(inputStr));
                }else if (inputStr[1] == 'f')
                {
                    string[] strings = inputStr.Split(' ');
                    int[] idArray = new int[3];
                    for (int i = 1; i < 4; i++)
                    {
                        int id = int.Parse(strings[1].Split('/')[0]);
                        idArray[i - 1] = id;
                    }
                    faces.Add(new Face(new []{verts[idArray[0]], verts[idArray[1]], verts[idArray[2]]}));
                }
            }

            return faces;
        }
    }
}