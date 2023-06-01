<script setup>

	import { SchoolHoursModel } from '../model/SchoolHoursModel'
	import { StudentFormService } from '../services/StudentFormService'
	import { ref, computed } from 'vue';

	const schoolHours = [
		new SchoolHoursModel(7, 20, 8, 5),
		new SchoolHoursModel(8, 5, 9, 0),
		new SchoolHoursModel(9, 0, 9, 55),
		new SchoolHoursModel(9, 55, 10, 50),
		new SchoolHoursModel(10, 50, 11, 45),
		new SchoolHoursModel(11, 45, 12, 50),
		new SchoolHoursModel(12, 50, 13, 45),
		new SchoolHoursModel(13, 45, 14, 40),
		new SchoolHoursModel(14, 40, 15, 35),
		new SchoolHoursModel(15, 35, 16, 30),
		new SchoolHoursModel(16, 30, 17, 25)
	]

	let numberOfLessonLeft = 2;
	const today = new Date();
	const yyyy = today.getFullYear();
	let mm = today.getMonth() + 1; // Months start at 0!
	let dd = today.getDate();

	if (dd < 10) dd = '0' + dd;
	if (mm < 10) mm = '0' + mm;

	let getCurrentHour = (numOfHours) => {

		let output = []
		let index = 0

		let currentTime = today.getHours() + today.getMinutes() / 60
		
		schoolHours.forEach(element => {
			let breakDecimal = element.getBreakDecimal()
			let endLessonDecimal = element.getEndLessonDecimal()

			let isBetweenThisLesson = currentTime >= breakDecimal && currentTime <= endLessonDecimal
			
			if(isBetweenThisLesson){
				output.push(element)

				numberOfLessonLeft = schoolHours.length - schoolHours.indexOf(element)

				index = schoolHours.indexOf(element) + Number(numOfHours) - 1
				output.push(schoolHours[index])
			}
		});

		if(output.length < 2) return "";

		return `${output[0].breakHours}:${output[0].breakMinutes} - ${output[1].endLessonHours}:${output[1].endLessonMinutes}`;
	}

	let name = "";
	let surname = "";
	let studentClass = "";
	let date = mm + ' / ' + dd + ' / ' + yyyy;
	let numberOfHours = ref(1);
	let currentHour = computed(() => getCurrentHour(numberOfHours.value))
	let issues = ""
	let error = ref('')

	function sendData(){
		if(name == "" || surname == "" || studentClass == "" || currentHour.value == ""){
			console.log(error)
			error.value = "niektóre pola nie zostały wypełnione!"
			console.log(error)
			return;
		}
		const studentFormService = new StudentFormService(`${name} ${surname}`, studentClass, date, numberOfHours, currentHour.value, issues)
		studentFormService.PostData()
		window.close()
	}

	

</script>

<template>
	<div class="flex flex-col w-full min-h-screen items-center justify-center bg-slate-50">
		<div class="relative flex items-end bg-white p-10 rounded-lg shadow-md space-x-10 overflow-hidden">
			<div class="flex h-full z-10">
				<img src="logo.svg" alt="logo ZSŁ">
			</div>
			<form class="grid grid-cols-2 justify-center gap-6 rounded-lg shadow-lg p-10 bg-[#07264e] text-white z-10">
				<h1 class="col-span-2 text-center uppercase tracking-wider">Formularz</h1>
					<div class="col-span-2">
						<p class="p-1">Imię</p>
						<input
						class="rounded focus:outline-none text-black py-1 px-2 w-full"
						type="text" 
						:value="name" 
						@input="event => name = event.target.value" 
						required
						/>
					</div>
					<div class="col-span-2">
						<p class="p-1">Nazwisko</p>
						<input
						class="rounded focus:outline-none text-black py-1 px-2 w-full"
						type="text" 
						:value="surname" 
						@input="event => surname = event.target.value" 
						required
						/>
					</div>
					<div>
						<p class="p-1">Klasa</p>
						<input
						class="rounded focus:outline-none text-black py-1 px-2"
						type="text" 
						:value="studentClass" 
						@input="event => studentClass = event.target.value"
						required 
						/>
					</div>
					<div>
						<p class="p-1">Liczba lekcji</p>
						<input
						class="rounded focus:outline-none text-black py-1 px-2 w-full"
						type="number" 
						:value="numberOfHours" 
						@input="event => {
							if(event.target.value < 0){
								numberOfHours = 0
							} 
							if(event.target.value > numberOfLessonLeft){
								numberOfHours = numberOfLessonLeft
							}
							else{
								numberOfHours = event.target.value
							}
						}"
						min="1"

						:max="numberOfLessonLeft"
						required
						/>
					</div>
					<div class="col-span-2">
						<p class="p-1">Uwagi</p>
						<textarea
						class="rounded focus:outline-none text-black py-1 px-2 resize-none w-full"
						rows="3"
						type="text" 
						:value="issues" 
						@input="event => issues = event.target.value" 
						></textarea>
					</div>
					<div>
						<!-- <p class="p-1">Data</p> -->
						<input
						class="rounded focus:outline-none bg-gray-50 text-[#061735] py-1 px-2 text-center"
						type="text" 
						:value="date" 
						@input="event => date = event.target.value" 
						disabled
						/>
					</div>
					<div>
						<!-- <p class="p-1">Godzina</p> -->
						<input
						class="rounded focus:outline-none bg-gray-50 text-[#061735] py-1 px-2 text-center"
						type="text" 
						:value="currentHour"
						disabled
						/>
					</div>
				<div class="flex justify-center col-span-2">
					<button type="button" class="w-64 tracking-widest uppercase bg-[#CC0001] rounded-full py-2" v-on:click="sendData()">Zapisz</button>
				</div>
			</form>
			<svg class="absolute -top-56 -left-52 -rotate-45 scale-150" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 1440 320"><path fill="#CC0001" fill-opacity="1" d="M0,64L24,74.7C48,85,96,107,144,138.7C192,171,240,213,288,224C336,235,384,213,432,186.7C480,160,528,128,576,144C624,160,672,224,720,213.3C768,203,816,117,864,106.7C912,96,960,160,1008,176C1056,192,1104,160,1152,138.7C1200,117,1248,107,1296,122.7C1344,139,1392,181,1416,202.7L1440,224L1440,0L1416,0C1392,0,1344,0,1296,0C1248,0,1200,0,1152,0C1104,0,1056,0,1008,0C960,0,912,0,864,0C816,0,768,0,720,0C672,0,624,0,576,0C528,0,480,0,432,0C384,0,336,0,288,0C240,0,192,0,144,0C96,0,48,0,24,0L0,0Z"></path></svg>
			<svg class="absolute -bottom-56 -right-40 -rotate-45 scale-150" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 1440 320"><path fill="#CC0001" fill-opacity="1" d="M0,64L24,74.7C48,85,96,107,144,138.7C192,171,240,213,288,224C336,235,384,213,432,186.7C480,160,528,128,576,144C624,160,672,224,720,213.3C768,203,816,117,864,106.7C912,96,960,160,1008,176C1056,192,1104,160,1152,138.7C1200,117,1248,107,1296,122.7C1344,139,1392,181,1416,202.7L1440,224L1440,320L1416,320C1392,320,1344,320,1296,320C1248,320,1200,320,1152,320C1104,320,1056,320,1008,320C960,320,912,320,864,320C816,320,768,320,720,320C672,320,624,320,576,320C528,320,480,320,432,320C384,320,336,320,288,320C240,320,192,320,144,320C96,320,48,320,24,320L0,320Z"></path></svg>
			
		</div>
		<div class="error" v-if="error != ''">
			{{ error }}
		</div>
	</div>
</template>

<style scoped>

</style>