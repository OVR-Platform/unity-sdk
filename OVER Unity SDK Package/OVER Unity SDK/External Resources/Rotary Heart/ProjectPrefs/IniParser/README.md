# UnityIniParser
Easy to use ini saving system that allows to store data in multiple sections. With this parser you can easily integrate ini files to your Unity games. The system has 2 different ini parsing methods Dictionary or List. The only difference is that Dictionary parsing will be faster, but it won't guarantee that the order of the data loaded is the same as the data stored. While the List is slower it will use the same order of the data stored.

This code is completely free to use for commercial or personal projects.

# Wiki
A detailed wiki can be found at the <A href="https://www.rotaryheart.com/Wiki/IniParser.html">Rotary Heart Wiki</a> section.

# Importing
Copy all the files to your project Asset folder or use Unity Package Manager git setup to import it. Add the following to your project packages.json `"rotaryheart.lib.iniparser": "https://gitlab.com/RotaryHeart-UnityShare/IniParser.git"`