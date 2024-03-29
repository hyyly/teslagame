﻿using System;
using System.Collections.Generic;
using System.Text;
using Tesla.Common;
using Tao.OpenGl;

namespace Tesla.GFX.ModelLoading
{
    public class Face
    {
        private Vector3f[] vertices;
        private Vector2f[] textureVertices;
        private Vector3f[] normalVertices;
        private int polygonType;

        public Face(Vector3f[] vertices, Vector2f[] textureVertices, Vector3f[] normalVertices, int polygonType)
        {
            this.vertices = vertices;
            this.textureVertices = textureVertices;
            this.normalVertices = normalVertices;
            this.polygonType = polygonType;
        }

        public int PolygonType()
        {
            return polygonType;
        }

        public void Draw()
        {
            if (polygonType != LoadableModel.currentPolygon)
            {
                Gl.glEnd();
                LoadableModel.Init(polygonType);
                LoadableModel.currentPolygon = polygonType;
            }

            for(int i = 0; i < vertices.Length; i++)
            {
                if (normalVertices.Length > 0)
                    Gl.glNormal3f(normalVertices[i].x, normalVertices[i].y, normalVertices[i].z);
                if (textureVertices.Length > 0)
                    Gl.glTexCoord2f(textureVertices[i].x, textureVertices[i].y);
                if (vertices.Length > 0)
                    Gl.glVertex3f(vertices[i].x, vertices[i].y, vertices[i].z);
            }
        }

        public override string ToString()
        {
            System.Text.StringBuilder sb = new StringBuilder();
            sb.Append("Face:\n");
            sb.Append(" Vertices: ");
            foreach (Vector3f p in vertices)
            {
                sb.Append("[" + p + "]");
            }
            sb.Append(" Texture Vertices: ");
            foreach (Vector2f t in textureVertices)
            {
                sb.Append("[" + t + "]");
            }
            sb.Append(" Normal Vertices: ");
            foreach (Vector3f n in normalVertices)
            {
                sb.Append("[" + n + "]");
            }
            sb.Append("End of Face");
            return sb.ToString();
        }
    }
}