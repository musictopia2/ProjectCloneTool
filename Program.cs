await BuildHookRunner.RunAsync(args, async payLoad =>
{

    string oldProjectFolder = payLoad.ProjectDir;

    string rootFolder = Directory.GetParent(oldProjectFolder.TrimEnd(Path.DirectorySeparatorChar))!.FullName;
    string oldName = payLoad.ProjectName;


    Console.Write("New Project Name: ");
    string newName = Console.ReadLine()!;

    string newFolder = Path.Combine(rootFolder, newName);


    string oldCsprojPath = Path.Combine(oldProjectFolder, $"{oldName}.csproj");
    string newCsprojPath = Path.Combine(newFolder, $"{newName}.csproj");

    string oldSlnxPath = Path.Combine(oldProjectFolder, $"{oldName}.slnx");
    string newSlnxPath = Path.Combine(newFolder, $"{newName}.slnx");

    await hh1.CopyFilesAsync(oldProjectFolder, newFolder, oldCsprojPath, newCsprojPath, oldSlnxPath, newSlnxPath, oldName, newName);


    Console.WriteLine("Check So Far");



});