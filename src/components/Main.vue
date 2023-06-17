<template>
    <main class="container">
        <article class="card disclaimer-card">
            <header class="d-flex disclaimer-header">
                <span>DISCLAIMER</span>
                <div class="close-indicator" type="button" data-bs-toggle="collapse" data-bs-target="#collapseDisclaimer"
                    aria-expanded="true" aria-controls="collapseDisclaimer">
                    <svg xmlns="http://www.w3.org/2000/svg" width="14" height="10" role="presentation"
                        @click="showDisclaimer = !showDisclaimer"
                        :class="showDisclaimer ? 'showDisclaimer' : 'hideDisclaimer'">
                        <path
                            d="M9.211364 7.59931l4.48338-4.867229c.407008-.441854.407008-1.158247 0-1.60046l-.73712-.80023c-.407008-.441854-1.066904-.441854-1.474243 0L7 5.198617 2.51662.33139c-.407008-.441853-1.066904-.441853-1.474243 0l-.737121.80023c-.407008.441854-.407008 1.158248 0 1.600461l4.48338 4.867228L7 10l2.211364-2.40069z">
                        </path>
                    </svg>
                </div>
            </header>
            <div class="content-disclaimer collapse show" id="collapseDisclaimer">
                <p class="my-0">
                    Disclaimer body
                </p>
            </div>
        </article>
        <article class="row justify-content-between my-4">
            <section class="col-md-4">
                <div class="card mb-4">
                    <form class="card-body d-flex flex-column" v-on:submit.prevent="getCurrencyRate">
                        <div class="mb-1">
                            <label class="mb-1" for="currency-select"> Select the currency:</label>
                            <v-select :options="listOfDisplayNames" label="displayName"
                                :reduce="currency => currency.isoCode" v-model="input.isoCode" :clearable="false"
                                placeholder="Please select the currency..." @input="resultState = ResultStateEnum.Initial">
                                <template #search="{ attributes, events }">
                                    <input class="vs__search" :required="!input.isoCode" v-bind="attributes"
                                        v-on="events" />
                                </template>
                            </v-select>
                        </div>
                        <div class="my-1">
                            <label class="mb-1"> Enter the amount:</label>
                            <InputNumber inputClass="form-control" v-model.number="input.amount" :minFractionDigits="2"
                                :maxFractionDigits="5" :min="1" placeholder="Enter the amount..." />
                        </div>
                        <div class="my-1">
                            <label class="mb-1"> Enter the transaction date:</label>
                            <input class="form-control" type="date" v-model="input.transactionDate" required />
                        </div>
                        <div class="mt-3">
                            <div class="d-flex justify-content-between">
                                <button type="reset" class="btn btn-secondary" @click="reset">Reset</button>
                                <button type="submit" class="btn btn-submit">Submit</button>
                            </div>
                        </div>
                    </form>
                </div>
            </section>
            <article class="my-3">
                <div class="row align-items-center">
                    <div class="col-lg-3 py-2">
                        The nearest date preceding the transaction:
                    </div>
                    <div class="col-lg-1 py-2 ">
                        <strong>12.05.2022</strong>
                    </div>
                </div>
                <div class="row align-items-center">
                    <div class="col-lg-3 py-2">
                        Currency exchange rate for 1 unit:
                    </div>
                    <div class="col-lg-3 py-2">
                        <strong>0,055 PLN</strong>
                    </div>
                </div>
                <div class="row align-items-center">
                    <div class="col-lg-3 py-2">
                        Official archive on the entered date:
                    </div>
                    <div class="col-lg-1 py-2">
                        <a href="/">Link</a>
                    </div>
                </div>
            </article>
        </article>
    </main>
</template>

<script>
import 'bootstrap/js/dist/collapse';
import Vue from "vue";
import VueSelect from "vue-select";
import "vue-select/dist/vue-select.css";
import json from '../assets/listOfAvailableCurrencies.json';
import PrimeVue from 'primevue/config';
import InputNumber from 'primevue/inputnumber';

Vue.component("v-select", VueSelect);
Vue.use(PrimeVue);
Vue.component("InputNumber", InputNumber);

export default {
    data() {
        return {
            showDisclaimer: true,

            input: {
                amount: 1,
                isoCode: null,
                transactionDate: null,
            },

            currency: {
                quantity: null,
                rateDate: null,
                rate: null,
                link: null
            }
        }
    },
    computed: {
        listOfDisplayNames() {
            let list = [];
            json.currencies.forEach(element => {
                list.push(element);
            })
            return list;
        }
    },
    methods: {
        async getCurrencyRate() {
            let url = new URL("/api/CurrencyRateProvider");
            const params = { isoCode: this.input.isoCode, transactionDate: this.input.transactionDate };
            url.search = new URLSearchParams(params);
            const response = await fetch(url);
            const data = await response.json();

            this.currency.quantity = data.Quantity;
            this.currency.rateDate = data.RateDate;
            this.currency.rate = data.Rate;
            this.currency.link = data.Link;
        },
        reset() {
            this.input.isoCode = null;
        }
    }
};
</script>

<style>
.disclaimer-card {
    padding: 0.75rem;
    margin-top: 2rem;
    margin-bottom: 2rem;
    color: #664d03;
    border-color: #ffecb5;
    background-color: #fff3cd;
}

.disclaimer-header {
    font-weight: bold;
    padding-top: 0.25rem;
    padding-bottom: 0.25rem;
    justify-content: space-between;
}

.close-indicator {
    fill: #664d03;
    transform: rotate(180deg);
}

.showDisclaimer {
    transform: rotate(0deg);
    transition-timing-function: cubic-bezier(1, 0.5, 0.8, 1);
    transition-duration: var(--vs-transition-duration);
}

.hideDisclaimer {
    transform: rotate(-180deg);
    transition-timing-function: cubic-bezier(1, 0.5, 0.8, 1);
    transition-duration: var(--vs-transition-duration);
}

.content-disclaimer {
    transition-timing-function: cubic-bezier(1, 0.5, 0.8, 1);
    transition-duration: var(--vs-transition-duration);
}

.content-disclaimer a {
    font-weight: bold;
    --bs-link-color: #664d03;
    --bs-link-hover-color: var(--bs-dark);
}

.vs__selected-options {
    flex-wrap: nowrap;
    max-width: calc(100% - 25px);
    padding: 0;
}

.vs__selected {
    display: block;
    white-space: nowrap;
    text-overflow: ellipsis;
    max-width: 100%;
    overflow: hidden;
    margin: 0;
    padding: 0;
}

.vs__dropdown-toggle {
    justify-content: space-between;
    padding: .375rem .75rem;
    border-radius: .375rem;
    border: 1px solid #ced4da;
}

.vs__search {
    margin: 0;
    padding: 0;
}

.vs__search:focus {
    margin: 0;
    padding: 0;
}

.p-inputnumber {
    display: block;
}

.card .btn-submit {
    --bs-btn-color: #fff;
    --bs-btn-bg: #244a7c;
    --bs-btn-border-color: #244a7c;
    --bs-btn-hover-color: #fff;
    --bs-btn-hover-bg: #152e52;
    --bs-btn-hover-border-color: #152e52;
    --bs-btn-focus-shadow-rgb: 130, 138, 145;
    --bs-btn-active-color: #fff;
    --bs-btn-active-bg: #152e52;
    --bs-btn-active-border-color: #152e52;
    --bs-btn-active-shadow: inset 0 3px 5px rgba(0, 0, 0, 0.125);
}
</style>
