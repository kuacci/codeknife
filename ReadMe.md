# Code Knife

This repo just for code storage of leetcode and other interesting patterns.

This repo using CSX by default. Following are steps to setup VS Code with runtime.

1. Download ScriptCs from <http://scriptcs.net/>. Either binary or source code is ok, you can load source code then build it by yourself in visual studio 2017.
2. Install **scriptcsRunner** extension in VS code.
3. Set the scriptcsPath in VS Code setting, this field should be the location of scriptcs.exe. Or you can add this location to `Path` of `system environment variables`:

    ```json
    {
        //other settings
        "scriptcsRunner.scriptcsPath": "scriptcs"
    }
    ```

4. Create file with name like app.csx then VS Code can leverage OmniSharp extension to provide the intellisense support just like coding in visual studio.
5. Run the code by the command of `Execute with scriptcs` or the shortcut `Ctrl + Shift + R`

## Debug Script

1. Locate the script file which you want to debug
2. Bring up terminal window in the same directory, then run `scriptcs` to launch `scriptcs` as `Repl` mode
3. In `scriptcs`, type command `:openvs "test.csx"` to launch VS and the target script. Then you will get the same experience of VS to debug script file.
