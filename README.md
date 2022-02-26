# RandstalkerGUI
GUI for the Landstalker randomizer

# Randstalker
[Randstalker](https://github.com/Dinopony/randstalker)  is a randomizer for the famous Megadrive / Genesis classic "Landstalker : The Treasure of King Nole". It works on a US ROM of the game by randomizing item sources and altering the game so that it is more enjoyable in a randomizer format.


# Usage
GUI is still in early stages and quality of life features will be added in the near future.

## Setup
Create a **Randstalker** folder in the same folder as **RandstalkerGui.exe**, then copy/paste the [Randstalker](https://github.com/Dinopony/randstalker) files in it (Don't forget to place input rom named 'input.md' in this directory too). Now you can launch the GUI exe.

## Features

### Homepage

Used for generating a rom (once everything is setup), afterwards the [Randstalker](https://github.com/Dinopony/randstalker) log will appear below the button.

### Settings

Used to edit the preset and personal_settings json files.

In the **Preset tab**, beware of the **Starting items**, **Items distribution** and **Hints** sections as no securisation mechanism is implemented in the GUI.
In the **Personal Settings** tab, colors must respect the format "#RGB" with R, G and B respectively Red, Green and Blue which values goes from 0 (no color) to 9..A to F (Full color).

### Options

In the **Options** menu, you can change the language of the GUI and also configure the paths for various files (By default points to Randstalker subfolder)

### Tips and tricks

- Don't forget to use the save buttons after editing parameters (preset, personnal settings and user config)
- If you want to edit the json files by hand, please restart RandstalkerGUI for it to read the new values
- Don't forget to read the [Setup section](https://github.com/Hawkrex/RandstalkerGUI#Setup)