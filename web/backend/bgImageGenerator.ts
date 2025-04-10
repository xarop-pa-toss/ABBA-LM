import { CanvasRenderingContext2D, createCanvas } from "https://deno.land/x/canvas/mod.ts"


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

    // Grid cells are squares based on width, not height.
    const zoneRegularWidth: number = width / gridDivisions;
    let zoneEdgeSize: number = 0

    const gridDivisionLeftover: number = zoneRegularWidth % gridDivisions
    if (gridDivisionLeftover % gridDivisions != 0) {
        zoneEdgeSize = gridDivisionLeftover / 2
    }

    ctx.strokeStyle = "#ffaed7"
    ctx.lineWidth = lineWidth

    // Vertical Lines
    const verticalStartPos = zoneEdgeSize
    for (let i = 1; i < gridDivisions; i++) {
        const x = i * zoneRegularWidth;
        ctx.beginPath();
        ctx.moveTo(x, 0);
        ctx.lineTo(x, height);
        ctx.stroke()
    }

    // Horizontal lines
    const horizontalStartPos = zoneEdgeSize
    for (let i = 1; i < gridDivisions; i++) {
        let y = i === 1 ? horizontalStartPos : i * zoneRegularWidth - horizontalStartPos;

        ctx.beginPath();
        ctx.moveTo(0, y);
        ctx.lineTo(width, y);
        ctx.stroke()
    }

    return {zoneRegularWidth, zoneEdgeSize}
}
