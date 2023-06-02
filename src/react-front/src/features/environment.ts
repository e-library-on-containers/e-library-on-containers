// @ts-ignore
export const environment = {
    apiUrl: ((window["env" as any] as unknown as {apiUrl: string})["apiUrl"]) || "default"
};
