const packageJson = require('./package.json');
const fs = require('fs');
const versionFile = './src/version.ts';

console.log('Writing version file...');
console.log(`Application name is ${packageJson.name}`);
console.log(`Application version is ${packageJson.version}`);
fs.writeFile(versionFile, `export const VERSION = '${packageJson.name}@${packageJson.version}';\n`, { encoding: 'utf-8' }, err => {
  if (err) {
    console.error('An error occurred while generating the version file: ', err);
  } else {
    console.log('Version file successfully written.');
  }
});
