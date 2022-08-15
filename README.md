[![Continuous Integration](https://github.com/Hawkrex/RandstalkerGUI/actions/workflows/CI.yml/badge.svg)](https://github.com/Hawkrex/RandstalkerGUI/actions/workflows/CI.yml)
[![GitHub tag](https://img.shields.io/github/tag/Hawkrex/RandstalkerGUI?include_prereleases=&sort=semver&color=blue)](https://github.com/Hawkrex/RandstalkerGUI/releases/)
[![License](https://img.shields.io/badge/License-MIT-blue)](#license)

# RandstalkerGUI
GUI for the Landstalker randomizer

# Randstalker
[Randstalker](https://github.com/Dinopony/randstalker)  is a randomizer for the famous Megadrive / Genesis classic "Landstalker : The Treasure of King Nole". It works on a US ROM of the game by randomizing item sources and altering the game so that it is more enjoyable in a randomizer format.


# Usage
GUI is still in early stages and quality of life features will be added in the near future.

## Setup
- Download the lastest release of the GUI and extract its files anywhere you want.
- Download the lastest release of [Randstalker](https://github.com/Dinopony/randstalker) and extract its files anywhere you want. 
- Now you can launch the GUI. If you don't have a UserConfig file, a popup will ask for the various path needed for the GUI to work.

## Features

### Homepage

Used to **generate a seed**. You can choose the preset and personal settings you want. In the recap, you can specify a permalink (useful for races) and a customized name for the output rom. Once the seed is generated, you can copy the permalink by clicking on the related button.

### Settings

Used to edit the preset and personal_settings json files.

In the **Preset tab**, beware of the **Starting items**, **Items distribution** and **Hints** sections as no securisation mechanism is implemented in the GUI.


### Options

In the **Options** menu, you can change the language of the GUI and also configure the paths for various files.

### Tips and tricks

- Don't forget to use the save buttons after editing parameters (preset, personnal settings and user config)
- If you want to edit the json files by hand, please restart RandstalkerGUI for it to read the new values
- Don't forget to read the [Setup section](https://github.com/Hawkrex/RandstalkerGUI#Setup)
