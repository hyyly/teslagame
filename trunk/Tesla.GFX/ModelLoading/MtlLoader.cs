﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using Tao.OpenGl;

namespace Tesla.GFX.ModelLoading
{
    public class MtlLoader
    {
        private static NumberFormatInfo numformat;
        private Dictionary<string, Material> map;
        private List<float> ambient, diffuse, specular;
        float alpha, shininess;
        Texture texture = null;
        Material.IllumType illumType;
        string latestGroup;

        public MtlLoader()
        {
            numformat = new NumberFormatInfo();
            numformat.NumberDecimalSeparator = ".";
            latestGroup = null;
        }

        public Dictionary<string, Material> LoadFile(String fileName)
        {
            map = new Dictionary<string, Material>();
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            int lastS = fileName.LastIndexOf('/');
            string filePath = fileName.Substring(0, lastS+1);

            try
            {
                StreamReader reader = new StreamReader(fileStream);
                
                SetDefault();
                Regex regex = new Regex(@"[\s]+");
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine().Trim().ToLower();

                    if (line.StartsWith("newmtl"))
                    {
                        string[] splittedGroupLine = regex.Split(line);
                        if (latestGroup != null)
                            AddMaterial();
                        SetDefault();
                        latestGroup = splittedGroupLine[1];
                        
                        while (!reader.EndOfStream)
                        {
                            line = reader.ReadLine().Trim();
                            if (line.StartsWith("ka "))
                            {
                                string[] splitted = regex.Split(line);
                                for (int i = 1; i < splitted.Length; i++)
                                        ambient.Add(Convert.ToSingle(splitted[i], numformat));
                            }
                            if (line.StartsWith("ks "))
                            {
                                string[] splitted = regex.Split(line);
                                for (int i = 1; i < splitted.Length; i++)
                                        specular.Add(ToFloat(splitted[i]));
                            }
                            if (line.StartsWith("kd "))
                            {
                                string[] splitted = regex.Split(line);
                                for (int i = 1; i < splitted.Length; i++)
                                        diffuse.Add(ToFloat(splitted[i]));
                            }
                            if (line.StartsWith("d ") || line.StartsWith("Tr "))
                            {
                                string[] splitted = regex.Split(line);
                                alpha = ToFloat(splitted[1]);
                            }
                            if (line.StartsWith("ns "))
                            {
                                string[] splitted = regex.Split(line);
                                shininess = ToFloat(splitted[1]);
                            }
                            if (line.StartsWith("illum "))
                            {
                                string[] splitted = regex.Split(line);
                                int num = ToInt(splitted[1]);
                                if (num == 1)
                                    illumType = Material.IllumType.FLAT;
                                else if (num == 2)
                                    illumType = Material.IllumType.SPECULAR;
                            }
                            if ((line.StartsWith("map_kd") || line.StartsWith("map_ks")))
                            {
                                string[] splitted = regex.Split(line);
                                if(splitted.Length>1)
                                    texture = Texture.CreateFromFile(filePath + splitted[1]);
                            }
                        }
                    }
                }
                AddMaterial();
            }
            finally
            {
                fileStream.Close();
            }
            return map;
        }

        public void AddMaterial()
        {
            if (latestGroup != null)
            {
                ambient.Add(alpha);
                diffuse.Add(alpha);
                specular.Add(alpha);
                map.Add(latestGroup, new Material(ambient.ToArray(), diffuse.ToArray(), specular.ToArray(),
                alpha, shininess, illumType, texture));
            }
        }

        private float ToFloat(string str)
        {
            return Convert.ToSingle(str, numformat);
        }

        private int ToInt(string str)
        {
            return Convert.ToInt16(str, numformat);
        }

        private void SetDefault()
        {
            ambient = new List<float>();
            diffuse = new List<float>();
            specular = new List<float>();
            alpha = 1.0f;
            shininess = 0.0f;
            texture = null;
            illumType = Material.IllumType.FLAT;
        }
    }
}