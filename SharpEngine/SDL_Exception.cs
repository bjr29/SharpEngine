namespace SharpEngine {
    /// <summary>
    /// An exception for when engine's window api has thrown an exception.
    /// </summary>
    public class SDL_Exception : Exception {
        internal SDL_Exception(string message) : base($"SDL has thrown an exception: {message}") { }
    }
}
