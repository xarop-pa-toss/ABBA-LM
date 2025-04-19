import { CanvasRenderingContext2D, createCanvas } from "https://deno.land/x/canvas@v1.4.2/mod.ts"


export function generateBackgroundImage(width: number, height: number, gridDivisions: number) {
    const canvas = createCanvas(width, height);
    const ctx = canvas.getContext("2d");

    // Background color and test square
    ctx.fillStyle = "#2d1a5e";
    ctx.fillRect(0, 0, width, height);

    // The generator creates stars, starting from the top left border [0,0] and goes by row.
    // Each star is created with an invisible shield around it that is 20x20 where no other star can exist.
    // There is an ever increasing chance of a star being created for each pixel traversed. The chance resets to base value when the star is created.
    drawGrid(ctx, width, height, gridDivisions, 0.5)


    const buffer = canvas.toBuffer("image/png");
    return buffer;
}

function drawGrid(ctx: CanvasRenderingContext2D, width: number, height: number, gridDivisions: number, lineWidth: number) {    
    ctx.strokeStyle = "#ffaed7"
    ctx.lineWidth = lineWidth

    // Grid cells are squares. Width was chosen as the base measurement.
    const zoneWidth: number = width / gridDivisions;
    const zoneWidthLeftover: number = width % gridDivisions;
    const zoneEdgeSize: number = (zoneWidthLeftover !== 0) ? (zoneWidthLeftover / 2) : 0;

    console.log("zoneWidth: " + zoneWidth)
    console.log("zoneWidthLeftover: " + zoneWidthLeftover)

    // Border
    ctx.beginPath();
    ctx.moveTo(0, 0);
    ctx.lineTo(width, 0);
    ctx.stroke();
    ctx.beginPath();
    ctx.moveTo(0, 0);
    ctx.lineTo(0, height);
    ctx.stroke()

    // Vertical Lines
    for (let i = 1; i < gridDivisions; i++) {
        const x = i * zoneWidth + zoneWidthLeftover;
        ctx.beginPath();
        ctx.moveTo(x, 0);
        ctx.lineTo(x, height);
        ctx.stroke()
    }
    
    // Horizontal lines
    for (let i = 1; i < gridDivisions; i++) {
        const y = i * zoneWidth - zoneWidthLeftover;
        ctx.beginPath();
        ctx.moveTo(0, y);
        ctx.lineTo(width, y);
        ctx.stroke()
    }

    return {zoneWidth, zoneWidthLeftover}
}
