import { Component,
         Input,
         ChangeDetectionStrategy,
         ChangeDetectorRef } from '@angular/core';
import { FormGroup, AbstractControl } from '@angular/forms';
import { ValidarCamposService } from '../validar-campos.service';
@Component({
  selector: 'app-input-select',
  templateUrl: './input-select.component.html',
  styleUrls: ['./input-select.component.css']
})
export class InputSelectComponent {

  @Input() titulo: string;
  @Input() formGroup: FormGroup;
  @Input() controlName: string;
  @Input() opcoes: Array<any>;
  @Input() style: string;
  @Input() isDisabled: boolean;
  @Input() isMultiple: boolean;
  @Input() ngChange;
  @Input() pai;
  @Input() texto;
  @Input() valor;

  constructor(public validacao: ValidarCamposService,
              private readonly changeDetectorRef: ChangeDetectorRef) {
    this.isDisabled = false;
    //this.ngChange = '';
    this.isMultiple = false;
    this.texto = '';
   }

  get formControl(): AbstractControl {
    return this.formGroup.controls[this.controlName];
  }

  update(): void {
    this.changeDetectorRef.markForCheck();
  }
}
