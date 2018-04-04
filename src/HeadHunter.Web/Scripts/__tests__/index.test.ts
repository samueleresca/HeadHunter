import { ZipCodeValidator } from '../index';
let myValidator = new ZipCodeValidator();
describe('Test Description', () => {
    it('TestName', () => {
        expect(myValidator.isAcceptable("ciao")).toBe(false);
    });
});