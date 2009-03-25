// Main.cs created with MonoDevelop
// User: topfs at 10:10 PM 9/27/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//
using System;
using System.Collections;
using Tesla.GFX;
using Tesla.GFX.Font;
using Tesla.Physics;
using Tesla.Common;
using Tesla.GFX.ModelLoading;
using Tesla.Audio;
using Tao.Sdl;
using Tao.OpenGl;

namespace Tesla
{
	class MainClass
	{
		static SDLWindow w;
        static Configuration c = new Configuration("config.dat");
		
		static AudioContext audioContext;
		static Listener listener; 
		
		static Weapon gun;
		static Ambient ambient;
		
		static void Initialize()
		{
			c  = new Configuration("config.dat");
			audioContext = new AudioContext(c);
			listener = new Listener();
			
			w = new SDLWindow(c);
			w.Add(buttonAction);
		}
		
		static void Deinitialize()
		{
			audioContext.Dispose();
		}
		
		static void LoadObjects()
		{
			w.setSkyBox(new SkyBox(new CubeMapTexture(c.defaultPath + "CubeMap/sky0", CubeMapType.None)));
			
			w.Add(new HUD(c.defaultPath));
			Vector3f va, vb, vc, vd;
			va = new Vector3f(-100.0f, 0.0f, -10.0f);
			vb = new Vector3f( 100.0f, 0.0f, -10.0f);
			vc = new Vector3f( 100.0f, 0.0f,  50.0f);
			vd = new Vector3f(-100.0f, 0.0f,  50.0f);
			w.Add(new GroundPlane(new BasicTexture(c.defaultPath + "Texture/Tile/chess0.jpg"), 16, 4, new Vector3f(0.0f, 0.0f, -20.0f), 200, 50.0f));
			w.Add(new Quad(new BasicTexture(c.defaultPath + "Texture/Foilage/Vine with alpha.png"), new Vector3f(-100, 0.65f, 5.001f), 10.0f, 20.0f, 1.0f));
		}
		
		static void LoadAudio()
		{
			ambient = new Ambient(c.defaultPath + "Ambient/BugsAndBirds.wav");
		}
		
		
		public static void Main(string[] args)
		{
			Initialize();
			
			LoadObjects();
			/* Load small Sounds before ambient as otherwise we get error creating buffer */
			gun = new Weapon(1000, 10, new Sound(c.defaultPath + "Audio/laserfire3.wav"));
			LoadAudio();

			
			
			w.getActiveCamera().getPosition().set(10.0f, 2.0f, 10.0f);
			w.getActiveCamera().rotateX(-3.0f);
			
			FPSCounter frameCounter = new FPSCounter();
			bool quitFlag = false;
			
			while(!quitFlag)
			{
				float frameTime = frameCounter.Update();
				quitFlag = w.Update(frameTime);
				updateAudio();
			}

			Deinitialize();
		}
		
		public static void updateAudio()
		{
			Camera c = w.getActiveCamera();
			listener.setPosition(c.getPosition());
			listener.setOrientation(c.getFrontVector(), c.getUpVector());
		}
		
		static int lastPressed;

		static void buttonAction(byte[] keyState, int numberKeys, float frameTime)
		{
			if (keyState[Sdl.SDLK_e] > 0 && gun.canFire())
				gun.Fire(w.getActiveCamera().getPosition() - new Vector3f(0.0f, 0.0f, 1.0f));
		}
	}
}