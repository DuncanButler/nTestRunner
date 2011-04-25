namespace nTestRunner.Interfaces
{
    public interface IProject
    {        
        string ProjectName { get; }
        string ProjectPath { get; }
        string TestFramework { get; }

        void Load(string fullProjectPath);
    }   
}