await BuildHookRunner.RunAsync(args, async payLoad =>
{

    string oldProjectFolder = payLoad.ProjectDir;
    Console.WriteLine(oldProjectFolder);

    string rootFolder = Directory.GetParent(oldProjectFolder.TrimEnd(Path.DirectorySeparatorChar))!.FullName;
    string oldName = payLoad.ProjectName;


    Console.Write("New Project Name: ");
    string newName = Console.ReadLine()!;

    string newFolder = Path.Combine(rootFolder, newName);


    string oldCsprojPath = Path.Combine(oldProjectFolder, $"{oldName}.csproj");
    string newCsprojPath = Path.Combine(newFolder, $"{newName}.csproj");


    await hh1.CopyFilesAsync(oldProjectFolder, newFolder, oldCsprojPath, newCsprojPath, oldName, newName);


    Console.WriteLine("Check So Far");



});