# SneakyCat
Snugging data into your renders.

## What is it?
SneakyCat is a Grasshopper plugin that lets you embed data into images using a technique called [Steganograpgy](https://en.wikipedia.org/wiki/Steganography).  
The plugin takes data to be embedded and embeds it into the least significant digits of the pixel data in the image. The changes to the image are so small that it is difficult to perceive with human eyes.  

## How to use?
Install the plugin as any [Grasshopper plugin](http://coder.the-bac.edu/?p=97).  
The `Purr` component embeds the data and `Hiss` component extracts it. You can optionally provide password to encrypt the data.  
Check out the example file to see how we embed and restor render settings into the render itself (secretly).  

### Embed
![./images/comp_embed.png]

### Extract
![./images/comp_extract.png]

## Team
- Amit Nambiar
- Joseph Wagner
- Vina Solimani
- Leland Curtis
- Zach S
This project was made as a part of the [AEC Tech 2018 hackathon](http://core.thorntontomasetti.com/aec-tech-2018/aec-tech-2018-hackathon/).  
