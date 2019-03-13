import { OnDestroy } from '@angular/core';
import { MonoTypeOperatorFunction, Observable, ReplaySubject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

/**
 * Une interface qui requiert la méthode `ngOnDestroy`.
 */
export interface WithOnDestroy extends OnDestroy {
  componentDestroyed$?: Observable<true>;
}

/**
 * Patch the component with unsubscribe behavior.
 *
 * @param component The component class (`this` conttext)
 * @return An observable representing the unsubscribe event
 */
export function componentDestroyed(component: WithOnDestroy): Observable<true> {
  if (component.componentDestroyed$) {
    return component.componentDestroyed$;
  }

  const originalNgOnDestroy: () => void | undefined = component.ngOnDestroy;
  if (typeof originalNgOnDestroy !== 'function') {
    throw new Error(`${component.constructor.name} does not implement OnDestroy interface.`);
  }

  const stop$ = new ReplaySubject<true>();

  component.ngOnDestroy = () => {
    originalNgOnDestroy.apply(component);
    stop$.next(true);
    stop$.complete();
  };

  return (component.componentDestroyed$ = stop$.asObservable());
}

/**
 * Un opérateur RxJS qui stop la souscription quand le composant est détruit (`ngOnDestroy`)
 *
 * @param component La classe du composant (`this`).
 */
export function untilComponentDestroyed<T>(component: WithOnDestroy): MonoTypeOperatorFunction<T> {
  return (source: Observable<T>) => source.pipe(takeUntil(componentDestroyed(component)));
}
