// Wind.cs created with MonoDevelop
// User: topfs at 5:57 PM 10/30/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tesla.Common;

namespace Tesla.GFX
{
	public class SimpleConstantForce : TemplateManipulator
	{
		float strength;
		Point3f direction;
		
		public SimpleConstantForce(Point3f direction, float strength)
		{
			this.direction = direction;
			this.strength  = strength;
		}

		public override void manipulate (Particle particle, Point3f deltaVelocity, Color4f deltaColor, ref float deltaLife)
		{
			deltaVelocity.add(direction);
			deltaVelocity.stretch(strength);
		}
	}
}
