import axios from "axios"
import { env } from "../../env/env"

export class AdminFormService {
	constructor(login, password){
		this.login = login
		this.password = password
	}
	postData() {
		const url = env.apiUrl + 'AdminLogin'

		const payload = {
			login: this.login,
			password: this.password
		}

		axios.get(url, payload).then(response => {
			return response.data
		}).catch(error => {
			return `error ${error}`
		})
	}
}