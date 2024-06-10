import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';

let connection = null;

export const getHubConnection = (token) => {
    if (connection) return connection;
    connection = new HubConnectionBuilder()
        .withUrl('https://localhost:7060/todoHub', {
            accessTokenFactory: () => token
        })
        .configureLogging(LogLevel.Information)
        .withAutomaticReconnect()
        .build();

    return connection;
};
