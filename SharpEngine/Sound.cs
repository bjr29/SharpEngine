using System;
using static SDL2.SDL;
using static SDL2.SDL_mixer;

namespace SharpEngine {
    public class Sound {
        #region Propeties
        public bool Paused { 
            get => Mix_Paused(Channel) == 1;
            set {
                if (value)
                    Mix_Pause(Channel);
                else
                    Mix_Resume(Channel);
            }
        }
        public int Volume { get => Mix_Volume(Channel, -1); set => _ = Mix_Volume(Channel, value); }
        public string Path { get; private set; }

        internal IntPtr SoundPtr { get; set; }
        internal int Channel { get; private set; }
        #endregion

        #region Constructors
        public Sound(string path) {
            SoundPtr = Mix_LoadWAV(path);
            Path = path;

            Debug.ErrorCheckSDL();
        }
        #endregion

        #region Methods
        public void Play(int loops = 0) {
            Channel = Mix_PlayChannel(-1, SoundPtr, loops);
            
            Debug.ErrorCheckSDL();
        }

        public void Stop() => _ = Mix_HaltChannel(Channel);
        #endregion
    }
}
