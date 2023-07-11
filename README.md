# challenge-code
Program will first prompt the user to enter the full path to an assets.txt file, though I suppose in theory this could be used with just about any JSON file. 
For the sake of this challenge, the user may search on any top level keys they wish, i.e. 'name', 'description', 'assetId', etc.
For example, to search for all assets whose names begin with 'newt' 
```
--name newt*
```
where ```*``` is of course the wild-card character. 

The program supports searches using multiple keys, for example
```
--name newt* --assetId *303*
```
will produce a list of all assets whose names begin with 'newt' and whose assetId contains the sequence '303'.

The program can also be used to search based on ```status```, using either the numerical values which represent each status or their textual counterparts,
i.e., the following inputs produce identical results:
```
--status 3
...
--status critical
```
Please keep in mind the keys you enter from the commandline are case-sensitive.
