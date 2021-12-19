using System;
using static SDL2.SDL_mixer;

namespace SharpEngine {
    /// <summary>
    /// A sound that can be played.
    /// </summary>
    public class Sound {
        /// <summary>
        /// Whether the sound has been paused or not.
        /// </summary>
        public bool Paused { 
            get => Mix_Paused(Channel) == 1;
            set {
                if (value)
                    Mix_Pause(Channel);
                else
                    Mix_Resume(Channel);
            }
        }
        /// <summary>
        /// The volume of the audio (up to 128, which is default).
        /// </summary>
        public int Volume { get => Mix_Volume(Channel, -1); set => _ = Mix_Volume(Channel, value); }
        /// <summary>
        /// The volume of the audio.
        /// </summary>
        public string Path { get; private set; }

        internal IntPtr SoundPtr { get; set; }
        internal int Channel { get; private set; }

        /// <summary>
        /// Creates the sound from the path.
        /// </summary>
        /// <param name="path"></param>
        public Sound(string path) {
            SoundPtr = Mix_LoadWAV(path);
            Path = path;

            Debug.ErrorCheckSDL();
        }

        /// <summary>
        /// Plays the sound, with the specified amount of loops.
        /// </summary>
        /// <param name="loops">The amount of times to loop (-1 = infinite).</param>
        public void Play(int loops = 0) {
            Channel = Mix_PlayChannel(-1, SoundPtr, loops);
            
            Debug.ErrorCheckSDL();
        }

        /// <summary>
        /// Stops the audio entirely.
        /// </summary>
        public void Stop() => _ = Mix_HaltChannel(Channel);
    }
}
