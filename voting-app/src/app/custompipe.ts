import { Pipe,PipeTransform } from "@angular/core";
@Pipe({
    name : 'hasvote'
})
export class Custompipe implements PipeTransform {
    transform(value: any, ...args: any[]) {
       if(value)
       {
        return 'V';
       }
       else
       return 'X';
    }
}
