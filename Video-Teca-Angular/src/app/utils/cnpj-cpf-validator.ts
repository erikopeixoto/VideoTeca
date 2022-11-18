import { ValidationResult } from './validation-result.d';
import { FormControl } from '@angular/forms';
import { Util } from './util';

export class CNPJCPFValidator {
  private static validarCnpj(numCpfCnpj) {
    const dig1 = 0;
    const dig2 = 0;
    let i;

    if (numCpfCnpj === '') {
      return null;
    }

    // Elimina CNPJs invalidos conhecidos
    if (
      numCpfCnpj === '00000000000000' ||
      numCpfCnpj === '11111111111111' ||
      numCpfCnpj === '22222222222222' ||
      numCpfCnpj === '33333333333333' ||
      numCpfCnpj === '44444444444444' ||
      numCpfCnpj === '55555555555555' ||
      numCpfCnpj === '66666666666666' ||
      numCpfCnpj === '77777777777777' ||
      numCpfCnpj === '88888888888888' ||
      numCpfCnpj === '99999999999999'
    ) {
      return { cnpjcpfInvalido: true };
    }

    let tamanho = numCpfCnpj.length - 2;
    let numeros = numCpfCnpj.substring(0, tamanho);
    const digitos = numCpfCnpj.substring(tamanho);

    let soma = 0;
    let pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
      soma += numeros.charAt(tamanho - i) * pos;
      pos--;
      if (pos < 2) {
        pos = 9;
      }
    }
    let resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado !== Number(digitos.charAt(0))) {
      return { cnpjcpfInvalido: true };
    }

    tamanho = tamanho + 1;
    numeros = numCpfCnpj.substring(0, tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
      soma += numeros.charAt(tamanho - i) * pos;
      pos--;
      if (pos < 2) {
        pos = 9;
      }
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado !== Number(digitos.charAt(1))) {
      return { cnpjcpfInvalido: true };
    }

    return null;
  }

  private static validarCpf(numCpfCnpj) {
    let soma = 0;
    let resto;
    let i;

    if (numCpfCnpj === '') {
      return null;
    }

    if (
      numCpfCnpj === '00000000000' ||
      numCpfCnpj === '11111111111' ||
      numCpfCnpj === '22222222222' ||
      numCpfCnpj === '33333333333' ||
      numCpfCnpj === '44444444444' ||
      numCpfCnpj === '55555555555' ||
      numCpfCnpj === '66666666666' ||
      numCpfCnpj === '77777777777' ||
      numCpfCnpj === '88888888888' ||
      numCpfCnpj === '99999999999'
    ) {
      return { cnpjcpfInvalido: true };
    }

    for (i = 1; i <= 9; i++) {
      soma = soma + parseInt(numCpfCnpj.substring(i - 1, i), 10) * (11 - i);
    }
    resto = soma * 10 % 11;

    if (resto === 10 || resto === 11) {
      resto = 0;
    }
    if (resto !== parseInt(numCpfCnpj.substring(9, 10), 10)) {
      return { cnpjcpfInvalido: true };
    }

    soma = 0;
    for (i = 1; i <= 10; i++) {
      soma = soma + parseInt(numCpfCnpj.substring(i - 1, i), 10) * (12 - i);
    }
    resto = soma * 10 % 11;

    if (resto === 10 || resto === 11) {
      resto = 0;
    }
    if (resto !== parseInt(numCpfCnpj.substring(10, 11), 10)) {
      return { cnpjcpfInvalido: true };
    }

    return null;
  }

  static validaCNPJCPF(formCNPJCPF: FormControl): ValidationResult {
    const cnpjcpf = formCNPJCPF.value;

    if (!cnpjcpf) {
      return null;
    }
    const numCpfCnpj: any = Util.removerMascaraCPFCNPJ(cnpjcpf.toString());
    const strCpfCnpj: string = numCpfCnpj;

    if (+numCpfCnpj === 0) {
      return { cnpjcpfInvalido: true };
    }

    if (strCpfCnpj.length === 14) {
      return CNPJCPFValidator.validarCnpj(numCpfCnpj);
    } else if (strCpfCnpj.length === 11 && cnpjcpf.indexOf('/') === -1) {
      return CNPJCPFValidator.validarCpf(numCpfCnpj);
    }

    return null;
  }
}
