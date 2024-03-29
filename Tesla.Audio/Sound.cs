// SimpleSound.cs created with MonoDevelop
// User: topfs at 3:19 PM 2/6/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Collections.Generic;

using Tesla.Common;

namespace Tesla.Audio
{
	public class Sound : IDisposable
	{
		Queue<Source> sources;
		Buffer audioBuffer;
		public Sound(string fileName) : this(fileName, 1)
		{
		}
		
		public Sound(string fileName, int numberOfSourcesHint)
		{
			audioBuffer = new Buffer(fileName);
			sources = new Queue<Source>(numberOfSourcesHint);
		}
		
		public void play(Vector3f position)
		{
			Source source;
			if (sources.Count == 0 || sources.Peek().isPlaying())
				source = new Source(audioBuffer, 1.0f);
			else
				source = sources.Dequeue();

			source.play(position, false);
			
			sources.Enqueue(source);
		}

		public void Dispose ()
		{
			foreach (Source s in sources)
			{
				s.stop();
				s.Dispose();
			}
			
			audioBuffer.Dispose();
		}
		
	}
}
