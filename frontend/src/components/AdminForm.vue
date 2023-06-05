<script setup>

import { AdminFormService } from '../services/AdminFormService'
import { ref } from 'vue';

let login = ""
let password = ""

let error = ref("")

function sendData(){
	let adminFormService = new AdminFormService(login, password)
	let result = adminFormService.postData()
	if(result.includes("error")){
		error = result
	}
}

</script>

<template>
	<form>
		<div>
			login 
			<input
			type="text" 
			:value="login" 
			@input="event => login = event.target.value" 
			required
			/>
		</div>
		<div>
			hasło 
			<input
			type="password" 
			:value="password" 
			@input="event => password = event.target.value" 
			required
			/>
		</div>
		<button type="button" v-on:click="sendData()">send</button>
	</form>
	<div class="error" v-if="error != ''">
		{{ error }}
	</div>
</template>

<style scoped>

</style>