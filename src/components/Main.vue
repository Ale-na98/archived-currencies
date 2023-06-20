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
                            <InputNumber inputClass="form-control" v-model="input.amount" :minFractionDigits="2"
                                :maxFractionDigits="5" placeholder="Enter the amount..." />
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
            <section class="col search-result" v-if="isInitialState(resultState)">
                <div class="card justify-content-between result-card">
                    <h4>How to use this site</h4>
                    <div class="divider"></div>
                    <p class="mb-2">
                        This service allows you to find out the exchange rates of currencies in relation to polish zloty
                        (PLN), which are not available in the
                        <a href="https://www.podatki.gov.pl/kalkulatory-podatkowe/kalkulator-walut/">
                            official currency calculator
                        </a>. However, the exchange rates of such currencies can be found on the
                        <a href="https://nbp.pl">
                            NBP website
                        </a> in the section with
                        <a href="https://nbp.pl/statystyka-i-sprawozdawczosc/kursy/archiwum-kursow-srednich-tabela-b/">
                            archived exchange rates
                        </a>.
                    </p>
                    <p class="mb-3">
                        To find out the
                        <a href="https://www.podatki.gov.pl/pit/twoj-e-pit/pit-38-za-2022/#przeliczenie-z-walut-obcych">
                            exchange rate for the last working day preceding the day of receipt of income
                        </a>, select the currency and the date of receipt of income.
                    </p>
                    <p class="fs-6 mb-0">
                        <strong>Attention!</strong> For these currencies, NBP publishes the average exchange rate not
                        per day (as, for example, for the dollar or euro), but per week (4-5 rates per month).
                    </p>
                </div>
            </section>
            <section class="col search-result" v-else-if="isResultFoundState(resultState)">
                <div class="col card justify-content-between result-card">
                    <h4>Result</h4>
                    <div class="divider"></div>
                    <div class="d-flex align-items-center my-2">
                        <div class="col-5 col-sm-6 col-md-5 col-lg-6" title="The nearest date preceding the transaction">
                            Date:
                        </div>
                        <div class="col">
                            <strong>{{ currency.rateDate }}</strong>
                        </div>
                    </div>
                    <div class="d-flex align-items-center my-2">
                        <div class="col-5 col-sm-6 col-md-5 col-lg-6">
                            Rate:
                        </div>
                        <div class="col">
                            <strong>{{ currency.quantity }} {{ input.isoCode }} = {{ currency.rate }} PLN</strong>
                        </div>
                    </div>
                    <div class="d-flex align-items-center my-2">
                        <div class="col-5 col-sm-6 col-md-5 col-lg-6">
                            Amount:
                        </div>
                        <div class="col">
                            <strong>{{ makeDecimal(input.amount, 4) }} {{ input.isoCode }}</strong>
                        </div>
                    </div>
                    <div class="d-flex align-items-center my-2">
                        <div class="col-5 col-sm-6 col-md-5 col-lg-6">
                            Amount after calculation:
                        </div>
                        <div class="col">
                            <strong>{{ calculate() }} PLN</strong>
                        </div>
                    </div>
                    <div class="d-flex align-items-center my-2">
                        <div class="col">
                            Please find the official exchange rate by the <a :href="currency.link">link</a>.
                        </div>
                    </div>
                </div>
            </section>
            <section class="col search-result" v-else-if="isResultNotFoundState(resultState)">
                <div class="col card result-card">
                    <h4>Result</h4>
                    <div class="divider"></div>
                    <div class="alert alert-primary d-flex align-items-center">
                        <svg xmlns="http://www.w3.org/2000/svg" class="bi flex-shrink-0 me-2" width="16" height="16"
                            role="img" aria-label="Info:">
                            <path
                                d="M8 16A8 8 0 1 0 8 0a8 8 0 0 0 0 16zm.93-9.412-1 4.705c-.07.34.029.533.304.533.194 0 .487-.07.686-.246l-.088.416c-.287.346-.92.598-1.465.598-.703 0-1.002-.422-.808-1.319l.738-3.468c.064-.293.006-.399-.287-.47l-.451-.081.082-.381 2.29-.287zM8 5.5a1 1 0 1 1 0-2 1 1 0 0 1 0 2z" />
                        </svg>
                        <p class="m-0">
                            Sorry, there is no information about exchange rate for selected currency:
                            <strong>{{ input.isoCode }}</strong>.
                        </p>
                    </div>
                </div>
            </section>
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
import ResultStateEnum from "../enums/ResultStateEnum";

Vue.component("v-select", VueSelect);
Vue.use(PrimeVue);
Vue.component("InputNumber", InputNumber);

export default {
    data() {
        return {
            showDisclaimer: true,

            ResultStateEnum,
            resultState: ResultStateEnum.Initial,

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
            const params = { isoCode: this.input.isoCode, transactionDate: this.input.transactionDate };
            const response = await fetch("/api/CurrencyRateProvider?" + new URLSearchParams(params));
            const data = await response.json();

            if (!Object.keys(data).length) {
                this.resultState = ResultStateEnum.ResultNotFound;
            } else {
                this.currency.quantity = data.Quantity;
                this.currency.rateDate = data.RateDate;
                this.currency.rate = data.Rate;
                this.currency.link = data.Link;

                this.resultState = ResultStateEnum.ResultFound;
            }
        },
        calculate() {
            if (this.input.amount === null) {
                this.input.amount = 1;
            }
            return this.makeDecimal((this.input.amount * (this.currency.rate / this.currency.quantity)), 2);
        },
        makeDecimal(number, fractionDigits) {
            return Number.parseFloat(number).toFixed(fractionDigits);
        },
        reset() {
            this.input.isoCode = null;
            this.input.amount = 1;
            this.resultState = ResultStateEnum.Initial;
        },
        isInitialState(resultState) {
            return resultState === ResultStateEnum.Initial;
        },
        isResultFoundState(resultState) {
            return resultState === ResultStateEnum.ResultFound;
        },
        isResultNotFoundState(resultState) {
            return resultState === ResultStateEnum.ResultNotFound;
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

.search-result {
    display: flex;
    justify-content: center;
    font-size: 17px;
}

.result-card {
    --bs-card-border-width: 0;
    padding: 0.75rem;
    margin-bottom: 1.5rem;
}

.result-card a {
    font-weight: bold;
    --bs-link-color: var(--bs-dark);
    --bs-link-hover-color: #244a7c;
}

.divider {
    width: 100%;
    opacity: .5;
    border-bottom: 2px solid #152e52;
    margin-bottom: 0.5rem;
}

.bi {
    fill: currentColor;
}
</style>
