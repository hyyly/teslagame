// Quad2D.cs created with MonoDevelop
// User: topfs at 4:42 PM 2/8/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tao.OpenGl;

namespace Tesla.GFX
{
	
	
	public class Quad2D : Drawable2D
	{
		Texture texture;
		float x, y;
		
		public Quad2D(Texture texture, float x, float y)
		{
			this.texture = texture;
			this.x = x;
			this.y = y;
		}

		public void Draw (float frameTime)
		{
			texture.Bind();
            Gl.glTranslatef(x, y, 0.0f);
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glTexCoord2f(0, 0); Gl.glVertex3f(0.0f, 0.0f, 0);
            Gl.glTexCoord2f(1, 0); Gl.glVertex3f(0.0f + texture.Width(), 0.0f, 0);
            Gl.glTexCoord2f(1, -1); Gl.glVertex3f(0.0f + texture.Width(), 0.0f + texture.Height(), 0);
            Gl.glTexCoord2f(0, -1); Gl.glVertex3f(0.0f, 0.0f + texture.Height(), 0);
            Gl.glEnd();
            Gl.glTranslatef(-x, -y, 0.0f);
		}
	}
}
