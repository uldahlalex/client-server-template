/* eslint-disable */
/* tslint:disable */
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

export interface DiagnosisResponseDto {
  /** @format guid */
  id: string;
  /** @format date-time */
  diagnosisDate: string | null;
  diseaseResponseDto: DiseaseResponseDto;
  doctorResponseDto: DoctorResponseDto;
}

export interface DiseaseResponseDto {
  /** @format guid */
  id: string;
  name: string;
  severity: string;
}

export interface DoctorResponseDto {
  /** @format guid */
  id: string;
  name: string;
  specialty: string;
  /** @format int32 */
  yearsExperience: number | null;
}

export interface CreateDiagnosisDto {
  /** @format guid */
  doctorId: string;
  /** @format guid */
  patientId: string;
  /** @format guid */
  diseaseId: string;
}

export interface PatientResponseDto {
  address: string | null;
  gender: boolean;
  /** @format guid */
  id: string;
  diagnosisResponseDtos: DiagnosisResponseDto[];
  /** @format date */
  birthdate: string;
  name: string;
}

export interface CreatePatientDto {
  name: string;
  /** @format date */
  birthdate: string;
  gender: boolean;
  address: string | null;
}

export interface UpdatePatientDto {
  /** @format guid */
  id: string;
  name: string;
  /** @format date */
  birthdate: string;
  gender: boolean;
  address: string | null;
}

export interface LoginResponse {
  jwt: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterResponse {
  email: string;
  name: string;
}

export interface RegisterRequest {
  email: string;
  password: string;
  name: string;
}

export interface AuthUserInfo {
  username: string;
  isAdmin: boolean;
  canPublish: boolean;
}

import type { AxiosInstance, AxiosRequestConfig, AxiosResponse, HeadersDefaults, ResponseType } from "axios";
import axios from "axios";

export type QueryParamsType = Record<string | number, any>;

export interface FullRequestParams extends Omit<AxiosRequestConfig, "data" | "params" | "url" | "responseType"> {
  /** set parameter to `true` for call `securityWorker` for this request */
  secure?: boolean;
  /** request path */
  path: string;
  /** content type of request body */
  type?: ContentType;
  /** query params */
  query?: QueryParamsType;
  /** format of response (i.e. response.json() -> format: "json") */
  format?: ResponseType;
  /** request body */
  body?: unknown;
}

export type RequestParams = Omit<FullRequestParams, "body" | "method" | "query" | "path">;

export interface ApiConfig<SecurityDataType = unknown> extends Omit<AxiosRequestConfig, "data" | "cancelToken"> {
  securityWorker?: (
    securityData: SecurityDataType | null,
  ) => Promise<AxiosRequestConfig | void> | AxiosRequestConfig | void;
  secure?: boolean;
  format?: ResponseType;
}

export enum ContentType {
  Json = "application/json",
  FormData = "multipart/form-data",
  UrlEncoded = "application/x-www-form-urlencoded",
  Text = "text/plain",
}

export class HttpClient<SecurityDataType = unknown> {
  public instance: AxiosInstance;
  private securityData: SecurityDataType | null = null;
  private securityWorker?: ApiConfig<SecurityDataType>["securityWorker"];
  private secure?: boolean;
  private format?: ResponseType;

  constructor({ securityWorker, secure, format, ...axiosConfig }: ApiConfig<SecurityDataType> = {}) {
    this.instance = axios.create({ ...axiosConfig, baseURL: axiosConfig.baseURL || "http://localhost:5000" });
    this.secure = secure;
    this.format = format;
    this.securityWorker = securityWorker;
  }

  public setSecurityData = (data: SecurityDataType | null) => {
    this.securityData = data;
  };

  protected mergeRequestParams(params1: AxiosRequestConfig, params2?: AxiosRequestConfig): AxiosRequestConfig {
    const method = params1.method || (params2 && params2.method);

    return {
      ...this.instance.defaults,
      ...params1,
      ...(params2 || {}),
      headers: {
        ...((method && this.instance.defaults.headers[method.toLowerCase() as keyof HeadersDefaults]) || {}),
        ...(params1.headers || {}),
        ...((params2 && params2.headers) || {}),
      },
    };
  }

  protected stringifyFormItem(formItem: unknown) {
    if (typeof formItem === "object" && formItem !== null) {
      return JSON.stringify(formItem);
    } else {
      return `${formItem}`;
    }
  }

  protected createFormData(input: Record<string, unknown>): FormData {
    if (input instanceof FormData) {
      return input;
    }
    return Object.keys(input || {}).reduce((formData, key) => {
      const property = input[key];
      const propertyContent: any[] = property instanceof Array ? property : [property];

      for (const formItem of propertyContent) {
        const isFileType = formItem instanceof Blob || formItem instanceof File;
        formData.append(key, isFileType ? formItem : this.stringifyFormItem(formItem));
      }

      return formData;
    }, new FormData());
  }

  public request = async <T = any, _E = any>({
    secure,
    path,
    type,
    query,
    format,
    body,
    ...params
  }: FullRequestParams): Promise<AxiosResponse<T>> => {
    const secureParams =
      ((typeof secure === "boolean" ? secure : this.secure) &&
        this.securityWorker &&
        (await this.securityWorker(this.securityData))) ||
      {};
    const requestParams = this.mergeRequestParams(params, secureParams);
    const responseFormat = format || this.format || undefined;

    if (type === ContentType.FormData && body && body !== null && typeof body === "object") {
      body = this.createFormData(body as Record<string, unknown>);
    }

    if (type === ContentType.Text && body && body !== null && typeof body !== "string") {
      body = JSON.stringify(body);
    }

    return this.instance.request({
      ...requestParams,
      headers: {
        ...(requestParams.headers || {}),
        ...(type ? { "Content-Type": type } : {}),
      },
      params: query,
      responseType: responseFormat,
      data: body,
      url: path,
    });
  };
}

/**
 * @title My Title
 * @version 1.0.0
 * @baseUrl http://localhost:5000
 */
export class Api<SecurityDataType extends unknown> extends HttpClient<SecurityDataType> {
  api = {
    /**
     * No description
     *
     * @tags Diagnosis
     * @name DiagnosisCreateDiagnosis
     * @request POST:/api/Diagnosis
     * @secure
     */
    diagnosisCreateDiagnosis: (data: CreateDiagnosisDto, params: RequestParams = {}) =>
      this.request<DiagnosisResponseDto, any>({
        path: `/api/Diagnosis`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Patient
     * @name PatientCreatePatient
     * @request POST:/api/Patient
     * @secure
     */
    patientCreatePatient: (data: CreatePatientDto, params: RequestParams = {}) =>
      this.request<PatientResponseDto, any>({
        path: `/api/Patient`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Patient
     * @name PatientUpdatePatient
     * @request PUT:/api/Patient
     * @secure
     */
    patientUpdatePatient: (data: UpdatePatientDto, params: RequestParams = {}) =>
      this.request<PatientResponseDto, any>({
        path: `/api/Patient`,
        method: "PUT",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Patient
     * @name PatientGetAllPatients
     * @request GET:/api/Patient
     * @secure
     */
    patientGetAllPatients: (
      query?: {
        /**
         * @format int32
         * @default 10
         */
        limit?: number;
        /**
         * @format int32
         * @default 0
         */
        startAt?: number;
      },
      params: RequestParams = {},
    ) =>
      this.request<PatientResponseDto[], any>({
        path: `/api/Patient`,
        method: "GET",
        query: query,
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Auth
     * @name AuthLogin
     * @request POST:/api/auth/login
     * @secure
     */
    authLogin: (data: LoginRequest, params: RequestParams = {}) =>
      this.request<LoginResponse, any>({
        path: `/api/auth/login`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Auth
     * @name AuthRegister
     * @request POST:/api/auth/register
     * @secure
     */
    authRegister: (data: RegisterRequest, params: RequestParams = {}) =>
      this.request<RegisterResponse, any>({
        path: `/api/auth/register`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Auth
     * @name AuthLogout
     * @request POST:/api/auth/logout
     * @secure
     */
    authLogout: (params: RequestParams = {}) =>
      this.request<File, any>({
        path: `/api/auth/logout`,
        method: "POST",
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Auth
     * @name AuthUserInfo
     * @request GET:/api/auth/userinfo
     * @secure
     */
    authUserInfo: (params: RequestParams = {}) =>
      this.request<AuthUserInfo, any>({
        path: `/api/auth/userinfo`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),
  };
}
