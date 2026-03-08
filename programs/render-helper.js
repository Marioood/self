/********** small, useful functions ***********/
//trig functions in degrees
function sin(theta) {
  return Math.sin(theta * (Math.PI / 180));
}
function cos(theta) {
  return Math.cos(theta * (Math.PI / 180));
}
function scale(num, oMin, oMax, nMin, nMax) {
  //"scales" a specific number restrained to some bounds to a new bounds
  //ex: 0 in a range of -1 to 1 scaled to a range of 0 to 1 would be 0.5
  
  
  
  
  //------> partially functional? i think?
  
  
  
  
  
  
  oWidth = oMax - oMin;
  nWidth = nMax - nMin;
  offset = (nMin + nMax) - (oMin + oMax);
  return num * nWidth / oWidth + offset / 2;
}
function rand(min, max) {
  return Math.random() * (max - min) + min;
}
function noiseArray(length, min, max) {
  let arr = Array(length);
  
  for(let i = 0; i < length; i++) {
    arr[i] = rand(min, max);
  }
  return arr;
}
function mod(n, d) {
  return Math.abs(n % d);
}
function lerp(n1, n2, bias) {
  return n1 + bias * (n2 - n1)
}
function easeCos(n) {
  return Math.cos(n * Math.PI + Math.PI) / 2 + 0.5;
}
function smoothstep(x) {
  return (x * (6 * x - 15) + 10) * x * x * x;
}