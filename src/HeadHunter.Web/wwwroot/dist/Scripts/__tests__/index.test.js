"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const index_1 = require("../index");
let myValidator = new index_1.ZipCodeValidator();
describe('Test Description', () => {
    it('TestName', () => {
        expect(myValidator.isAcceptable("ciao")).toBe(false);
    });
});
//# sourceMappingURL=index.test.js.map