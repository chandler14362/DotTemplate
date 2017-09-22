StringTemplate.MSBuild
======================

StringTemplate.MSBuild is a MSBuild extension that renders StringTemplate4 templates before compilation. It is recommend you read the ST4 documentation [here](https://github.com/antlr/stringtemplate4/blob/master/doc/introduction.md). 

Creating Renderable Templates
-----------------------------

The first step in the process of creating a renderable template is creating a [template group](https://github.com/antlr/stringtemplate4/blob/master/doc/groups.md). Please note that single template files (```.st```) support is currently planned and not implemented. Template groups are pretty cool, they let you define templates to your heart's content while providing a sense of structure. Some features I highly recommend you look at are [regions](https://github.com/antlr/stringtemplate4/blob/master/doc/regions.md) and [group inheritance](https://github.com/antlr/stringtemplate4/blob/master/doc/inheritance.md). Letting StringTemplate.MSBuild know what templates to render is pretty simple. The only requirements are a defined ```render``` template within the group accompanied with an ouput file extension. Providing the file extension is best explained using the following examples C#: ```DbInterface.cs.stg``` python: ```messages.py.stg``` text: ```imrunningoutofideas.txt.stg```.

If you want to see a project that utilizes StringTemplate.MSBuild, I highly recommend you look at [Krypton.LibProtocol](https://github.com/toontown-archive/Krypton.LibProtocol/tree/develop/LibProtocol.Targets/CSharp/Src/Numericals). The C# target utilizes templates to render a set of structs that share a common set of interfaces.

Planned Features
----------------

- ```.st``` file support.
- Configurable output directories. (as of now rendered files are plopped in the same directory as their template)
