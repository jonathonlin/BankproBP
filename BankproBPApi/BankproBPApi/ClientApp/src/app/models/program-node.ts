import { CompanyProgramReadDTO } from "../services/api.client.generated";

export interface ProgramNode extends CompanyProgramReadDTO {
    children?: ProgramNode[];
}
