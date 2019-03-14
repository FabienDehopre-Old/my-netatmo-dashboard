import { LogLevelType } from './log-level';

export interface AppConfig {
  logLevel: LogLevelType;
  apiBaseUrl: string;
  auth0: {
    domain: string;
    clientId: string;
  };
}
