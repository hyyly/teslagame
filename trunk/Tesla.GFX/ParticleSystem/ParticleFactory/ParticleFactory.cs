// ParticleFactory.cs created with MonoDevelop
// User: topfs at 7:18 PM 10/31/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

using Tesla.Common;

namespace Tesla.GFX
{
	public interface ParticleFactory
	{
		void preDraw();
		void postDraw();
		Particle createParticle(Point3f emitterPosition, bool emitterUseDirection, Point3f emitterDirection); 
	}
}
