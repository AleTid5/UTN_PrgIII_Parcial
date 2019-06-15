import axios from "axios";
import { EventBus } from "@/eventBus"

const API = "http://localhost:50950/api";

const errorHandler = (error) => {
    if(error.request) {
        EventBus.$emit("axios-request-error", {
            status: false,
            errors: "Internal server error"
        });
    }

    return Promise.reject(error);
};

const successHandler = (response) => {
    if (response.data.status === false) {
        EventBus.$emit("axios-request-error", response.data);
        Promise.reject(response);
    }

    return response
};

const client = axios.create({
    baseURL: API,
    json: true,
    crossDomain: true,
    withCredentials: false
});

client.interceptors.response.use(
    response => successHandler(response),
    error => errorHandler(error)
);

export default {
    async execute(method, resource, data) {
        const config = {
            method,
            url: resource,
            data,
            handlerEnabled: true,
            headers: {
                "Access-Control-Allow-Origin": "http://localhost:8080",
                "Access-Control-Allow-Methods": "GET, POST, OPTIONS, PUT, PATCH, DELETE",
                "Access-Control-Allow-Headers": "Access-Control-Allow-Origin, Origin,Accept, X-Requested-With, Content-Type, Access-Control-Request-Method, Access-Control-Request-Headers,X-Access-Token,XKey,Authorization",
                'Content-Type': 'multipart/form-data',
            }
        };

        return client(config).then(req => {
            return req.data;
        });
    },

    // *****************************************************************************************************************
    // BOOKMARKS
    // *****************************************************************************************************************
    getMagicians() {
        return this.execute("GET", "/main");
    },

    getMagiciansData(id) {
        return this.execute("GET", "/main/" + id);
    },

    saveMagician(name, house, spells) {
        return this.execute("GET", "/main/save?name=" + name + "&house=" + house + "&spells=" + spells);
    },
};