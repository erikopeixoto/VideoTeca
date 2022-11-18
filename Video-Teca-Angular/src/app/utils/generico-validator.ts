import { ValidationResult } from './validation-result';
import { FormControl } from '@angular/forms';
import { Util } from './util';

export class GenericoValidator {
    static validarData(data: FormControl): ValidationResult {
        const dtc = data.value;
        if (!dtc) {
            return null;
        }
        if (dtc <= Date.now) {
            return null;
        }
        return { 'dataInvalida': true };
    }
}
