import { LogLevelType } from './log-level';

export interface AppConfig {
  logLevel: LogLevelType;
  auth0: {
    domain: string;
    clientId: string;
  };
}
