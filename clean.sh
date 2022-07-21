#!/bin/bash
# Purpose: Basic shell script to remove 'BIN' and 'OBJ' folders
# Author: Nasr Aldin {https://nasraldin.com/} under GPL v2.0+
# -----------------------------------------------------------------

echo CodeZero by Nasr Aldin - nasr2ldin@gmail.com
echo Deleting 'BIN' and 'OBJ' folders
echo Version: 1.0.0
echo

echo Deleting...
# Find and delete all directory recursively 'BIN' and 'OBJ' folders
find $(pwd) -type d \( -name "bin" -o -name "obj" \);
find $(pwd) -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} +;
echo BIN and OBJ folders have been successfully deleted.
