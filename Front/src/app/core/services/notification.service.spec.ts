import { TestBed } from '@angular/core/testing';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { NotificationService } from './notification.service';

describe('NotificationService', () => {
  let service: NotificationService;
  let mockNzNotification: jasmine.SpyObj<NzNotificationService>;

  beforeEach(() => {
    const spy = jasmine.createSpyObj('NzNotificationService', ['success', 'error', 'warning', 'info']);

    TestBed.configureTestingModule({
      providers: [
        NotificationService,
        { provide: NzNotificationService, useValue: spy }
      ]
    });
    service = TestBed.inject(NotificationService);
    mockNzNotification = TestBed.inject(NzNotificationService) as jasmine.SpyObj<NzNotificationService>;
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });


});
