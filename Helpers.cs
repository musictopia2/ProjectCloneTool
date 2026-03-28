namespace ProjectCloneTool;

internal static partial class Helpers
{


    // Folders to skip (build outputs, version control, IDE files)
    private static readonly HashSet<string> _skipFolders =
    [
        "bin",
        "obj",
        ".vs",
        ".git",
        ".idea"
    ];

    // File types where replacement should occur
    private static readonly HashSet<string> _replaceableExtensions =
    [
        ".cs",
        ".xaml",
        ".csproj",
        ".sln",
        ".config",
        ".xml",
        ".json",
        ".razor"
    ];

    public static async Task CopyFilesAsync(
        string sourceFolder,
        string targetFolder,
        string sourceCsproj,
        string targetCsproj,
        string oldName,
        string newName)
    {
        // Delete target if it already exists
        if (Directory.Exists(targetFolder))
        {
            Directory.Delete(targetFolder, recursive: true);
        }

        Directory.CreateDirectory(targetFolder);

        // Copy all files/folders recursively
        await CopyFolderRecursively(sourceFolder, targetFolder, oldName, newName, sourceCsproj, targetCsproj);

        // Copy the main csproj file last
        File.Copy(sourceCsproj, targetCsproj, overwrite: true);
    }

    private static async Task CopyFolderRecursively(
        string source,
        string target,
        string oldName,
        string newName,
        string sourceCsproj,
        string targetCsproj)
    {
        // Copy subfolders
        foreach (string dir in Directory.GetDirectories(source))
        {
            string folderName = Path.GetFileName(dir);
            if (_skipFolders.Contains(folderName))
            {
                continue;
            }

            string newDir = Path.Combine(target, folderName);
            Directory.CreateDirectory(newDir);
            await CopyFolderRecursively(dir, newDir, oldName, newName, sourceCsproj, targetCsproj);
        }

        // Copy files
        foreach (string file in Directory.GetFiles(source))
        {
            string fileName = Path.GetFileName(file);
            string newLocation = Path.Combine(target, fileName);

            // Skip main csproj, already handled separately
            if (file.Equals(sourceCsproj, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            string ext = Path.GetExtension(file);

            if (_replaceableExtensions.Contains(ext))
            {
                // Read, replace oldName -> newName, then write
                string contents = await File.ReadAllTextAsync(file);
                contents = contents.Replace(oldName, newName);
                await File.WriteAllTextAsync(newLocation, contents);
            }
            else
            {
                // Copy static files as-is
                File.Copy(file, newLocation, overwrite: true);
            }
        }
    }



}
