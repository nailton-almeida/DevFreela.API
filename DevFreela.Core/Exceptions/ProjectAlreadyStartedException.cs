namespace DevFreela.Core.Exceptions
{
    internal class ProjectAlreadyStartedException : Exception
    {
        public ProjectAlreadyStartedException() : base("Project already started")
        {

        }
    }
}
