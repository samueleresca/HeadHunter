﻿class ZipCodeValidator {
   public isAcceptable(s: string) {
        return s.length === 5;
    }
}
export { ZipCodeValidator };
export { ZipCodeValidator as mainValidator };