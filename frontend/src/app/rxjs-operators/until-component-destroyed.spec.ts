import { Component, OnDestroy, OnInit } from '@angular/core';
import { fakeAsync, tick } from '@angular/core/testing';
import { interval, Subscription } from 'rxjs';

import { untilComponentDestroyed } from './until-component-destroyed';

// tslint:disable:max-classes-per-file no-life-cycle-call

@Component({
  selector: 'app-test-host',
  template: ``,
})
class TestHostComponent implements OnDestroy, OnInit {
  stub: jasmine.Spy = jasmine.createSpy('stub');
  myInterval!: Subscription;

  ngOnInit(): void {
    this.myInterval = interval(200)
      .pipe(untilComponentDestroyed(this))
      .subscribe((v: number) => {
        this.stub(v);
      });
  }

  ngOnDestroy(): void {
    /* DO NOT DELETE */
  }
}

@Component({
  selector: 'app-test-host-double',
  template: ``,
})
class TestHostDoubleComponent implements OnDestroy, OnInit {
  stub1: jasmine.Spy = jasmine.createSpy('stub1');
  stub2: jasmine.Spy = jasmine.createSpy('stub2');
  myInterval!: Subscription;
  mySecondInterval!: Subscription;

  ngOnInit(): void {
    this.myInterval = interval(200)
      .pipe(untilComponentDestroyed(this))
      .subscribe((v: number) => {
        this.stub1(v);
      });
    this.mySecondInterval = interval(200)
      .pipe(untilComponentDestroyed(this))
      .subscribe((v: number) => {
        this.stub2(v);
      });
  }

  ngOnDestroy(): void {
    /* DO NOT DELETE */
  }
}

@Component({
  selector: 'app-test-host-without-destroy',
  template: ``,
})
class TestHostWithoutOnDestroyComponent implements OnInit {
  stub: jasmine.Spy = jasmine.createSpy('stub');
  myInterval!: Subscription;

  ngOnInit(): void {
    this.myInterval = interval(200)
      .pipe(untilComponentDestroyed(this as any))
      .subscribe((v: number) => {
        this.stub(v);
      });
  }
}

describe(`untilComponentDestroyed`, () => {
  it(`should cancel an observable stream during the destroy cycle`, fakeAsync(() => {
    const testComponent = new TestHostComponent();

    testComponent.ngOnInit();
    tick(610);

    testComponent.ngOnDestroy();
    tick(1000);

    expect(testComponent.stub).toHaveBeenCalledTimes(3);
  }));

  it(`should cancel an observable stream during the destroy cycle`, fakeAsync(() => {
    const testComponent = new TestHostDoubleComponent();

    testComponent.ngOnInit();
    tick(610);

    testComponent.ngOnDestroy();
    tick(1000);

    expect(testComponent.stub1).toHaveBeenCalledTimes(3);
    expect(testComponent.stub2).toHaveBeenCalledTimes(3);
  }));

  it('should throw an error if the component does not implement OnDestroy interface', fakeAsync(() => {
    const testComponent = new TestHostWithoutOnDestroyComponent();
    expect(() => testComponent.ngOnInit()).toThrowError('TestHostWithoutOnDestroyComponent does not implement OnDestroy interface.');
    tick(610);
    expect(testComponent.stub).not.toHaveBeenCalled();
  }));
});
