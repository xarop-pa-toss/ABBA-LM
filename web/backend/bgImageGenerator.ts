import { createCanvas } from "https://deno.land/x/canvas/mod.ts"

const canvas = createCanvas(800, 600)
const ctx = canvas.getContext("2d")

// Background color
ctx.fillStyle = "#251f38"

// The generator creates stars, starting from the top left border [0,0] and goes by row.
// Each star is created with an invisible shield around it that is 20x20 where no other star can exist.
// There is an ever increasing chance of a star being created for each pixel traversed. The chance resets to base value when the star is created.
