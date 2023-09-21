# Parser Json
The app looks for a value in json files by a list of keys.
## How to use
Place executable file in a folder with json files or set the path to files right after running the app.
Place a key sequence separated by comma (',').
App shows a list of files with value by the path. None is shown if the key sequence is not found.
## Example
Test.json - {"firstLevelKey": {"secondLevelKey":"A Value"} }
Key sequence: firstLevelKey,secondLevelKey.
Result: Test.json: A Value.
