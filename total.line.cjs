const fs = require('fs');
const path = require('path');

const directoryPath = '/home/ersinware/repositories/monopoly';
const fileExtension = '.cs';

function countLines(filePath) {
    const content = fs.readFileSync(filePath, 'utf8');
    const lines = content.split('\n');
    return lines.length;
}

function processFiles(dir, ext) {
    let totalLines = 0;

    const files = fs.readdirSync(dir);

    files.forEach(file => {
        const filePath = path.join(dir, file);

        if (fs.statSync(filePath).isDirectory()) {
            // Recursively process subdirectories
            totalLines += processFiles(filePath, ext);
        } else if (path.extname(file) === ext) {
            // Process only files with the specified extension
            totalLines += countLines(filePath);
        }
    });

    return totalLines;
}

const totalLines = processFiles(directoryPath, fileExtension);

console.log(`Total lines in .cs files under ${directoryPath}: ${totalLines}`);
